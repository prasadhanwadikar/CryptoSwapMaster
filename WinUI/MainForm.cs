using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using CryptoSwapMaster.BinanceApiAdapter;
using CryptoSwapMaster.BinanceApiAdapter.Enums;
using CryptoSwapMaster.BinanceApiAdapter.Types;
using CryptoSwapMaster.Data;
using CryptoSwapMaster.Data.Entities;
using CryptoSwapMaster.Data.Enums;

namespace CryptoSwapMaster.WinUI
{
    public partial class MainForm : Form
    {
        private readonly object _quoteUpdateLock;
        private readonly System.Windows.Forms.Timer _timer;
        private readonly BackgroundWorker _bgwRefreshQuotes;
        private readonly Repository _db;
        private BinanceApiClient _binance;
        
        private User _user;
        private List<string> _allAssets;
        private List<string> _quoteAssets;
        private AccountInfo _accountInfo;
        private List<Order> _openOrders;

        private string _baseAsset = string.Empty;
        private decimal _baseAssetFreeBalance = 0M;
        private int _pool = 0;
        private int _group = 0;
        private string _quoteAsset = string.Empty;
        private decimal _baseQty = 0M;
        private decimal _quoteQty = 0M;

        public MainForm()
        {
            InitializeComponent();

            _quoteUpdateLock = new object();
            _quoteAssets = new List<string>();

            _bgwRefreshQuotes = new BackgroundWorker {WorkerSupportsCancellation = true};
            _bgwRefreshQuotes.DoWork += RefreshData;
            _bgwRefreshQuotes.RunWorkerCompleted += RefreshDataCompleted;

            _timer = new System.Windows.Forms.Timer
            {
                Interval = 10000
            };
            _timer.Tick += TimerOnElapsed;

            _db = new Repository();
            _binance = new BinanceApiClient("", "", 30000, 9000);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                btnChangeBotStatus.Enabled = false;
                lblIPValue.Text = new WebClient().DownloadString("https://ipinfo.io/ip").Trim();
                _user = _db.GetUser(lblIPValue.Text);
                if (_user == null)
                {
                    tpDashboard.Enabled = false;
                    tpOrdersHistory.Enabled = false;
                    tcSections.SelectedTab = tpSettings;
                    MessageBox.Show("Provide ApiKey and SecretKey to get started!", "Unknown IP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                txtApiKey.Text = _user.ApiKey;
                txtSecretKey.Text = _user.SecretKey;
                _binance.Reset(_user.ApiKey, _user.SecretKey, 30000, 9000);

                cbBaseAsset2.SelectedIndex = 0;
                cbOrderStatus.SelectedIndex = 0;

                LoadDashboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDashboard()
        {
            tpDashboard.Enabled = true;
            tpOrdersHistory.Enabled = true;
            tcSections.SelectedTab = tpDashboard;
            if (!_timer.Enabled) _timer.Start();
            if (_bgwRefreshQuotes.IsBusy) return;
            _bgwRefreshQuotes.RunWorkerAsync();
        }

        private void TimerOnElapsed(object sender, EventArgs e)
        {
            try
            {
                if (_bgwRefreshQuotes.IsBusy) return;
                _bgwRefreshQuotes.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshData(object sender, DoWorkEventArgs e)
        {
            _user = _db.GetUser(lblIPValue.Text);
            _accountInfo = _binance.AccountInfo;
            _allAssets = _accountInfo.Balances.Select(x => x.Asset).ToList();
            _quoteAssets = _db.GetQuoteAssets(_user.Id).Select(x => x.Asset).Where(y => _allAssets.Any(z => z == y)).ToList();
            
            var dtBalances = new DataTable("Balances");
            dtBalances.Columns.Add("Asset");
            dtBalances.Columns.Add("Balance");
            foreach (var asset in _quoteAssets)
                dtBalances.Columns.Add(asset);

            foreach (var balance in _accountInfo.Balances.Where(x => x.Free > 0))
            {
                if (_binance.IsInsufficientQty(balance.Asset, balance.Free)) continue;

                var newRow = dtBalances.Rows.Add();
                newRow["Asset"] = balance.Asset;
                newRow["Balance"] = balance.Free;
                Parallel.ForEach(_quoteAssets, (quoteAsset) =>
                {
                    var quoteQty = _binance.GetQuote(balance.Asset, quoteAsset, balance.Free, _accountInfo.TakerCommission);
                    lock (_quoteUpdateLock)
                    {
                        newRow[quoteAsset] = quoteQty;
                    }
                });
            }

            e.Result = dtBalances;
        }

        private void RefreshDataCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    if (e.Error.GetType() == typeof(BinanceException))
                    {
                        if (_timer.Enabled) _timer.Stop();
                        HandleBinanceException((BinanceException)e.Error);
                        return;
                    }
                    throw e.Error;
                }

                var dtBalances = (DataTable)e.Result;
                dgBalance.DataSource = dtBalances;

                cbBaseAsset.Items.Clear();
                foreach (var row in dtBalances.Rows)
                    cbBaseAsset.Items.Add(((DataRow)row)["Asset"]);

                if (cbBaseAsset.Items.Contains(_baseAsset))
                    cbBaseAsset.SelectedItem = _baseAsset;
                else
                    cbBaseAsset.SelectedIndex = 0;

                if (clbQuoteAssets.Items.Count != _allAssets.Count)
                {
                    clbQuoteAssets.Items.Clear();
                    foreach (var asset in _allAssets)
                        clbQuoteAssets.Items.Add(asset, _quoteAssets.Contains(asset));
                }

                UpdateBotStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateBotStatus()
        {
            switch (_user.BotStatus)
            {
                case BotStatus.StartRequested:
                    lblBotStatus.Text = "Starting ...";
                    lblBotStatus.ForeColor = Color.Blue;
                    btnChangeBotStatus.Text = "Stop Bot";
                    btnChangeBotStatus.Enabled = false;
                    break;

                case BotStatus.Running:
                    lblBotStatus.Text = "ON";
                    lblBotStatus.ForeColor = Color.Green;
                    btnChangeBotStatus.Text = "Stop Bot";
                    btnChangeBotStatus.Enabled = true;
                    break;

                case BotStatus.StopRequested:
                    lblBotStatus.Text = "Stopping ...";
                    lblBotStatus.ForeColor = Color.Blue;
                    btnChangeBotStatus.Text = "Start Bot";
                    btnChangeBotStatus.Enabled = false;
                    break;

                case BotStatus.Stopped:
                    lblBotStatus.Text = "OFF";
                    lblBotStatus.ForeColor = Color.Red;
                    btnChangeBotStatus.Text = "Start Bot";
                    btnChangeBotStatus.Enabled = true;
                    break;
            }
        }

        private void cbBaseAsset_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _baseAsset = cbBaseAsset.SelectedItem.ToString();

                RefreshOpenOrders();

                cbQuoteAsset.Items.Clear();
                foreach (var quoteAsset in _quoteAssets.Where(x => x != _baseAsset))
                    cbQuoteAsset.Items.Add(quoteAsset);

                if (cbQuoteAsset.Items.Contains(_quoteAsset))
                    cbQuoteAsset.SelectedItem = _quoteAsset;
                else
                    cbQuoteAsset.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshOpenOrders()
        {
            if (string.IsNullOrWhiteSpace(_baseAsset)) return;

            var balance = _accountInfo.Balances.FirstOrDefault(x => x.Asset == _baseAsset);
            _baseAssetFreeBalance = balance != null ? balance.Free : 0M;
            lblFreeBalValue.Text = _baseAssetFreeBalance.ToString();

            _openOrders = _db.GetOrders(_user.Id, OrderStatus.Open)
                .Where(x => x.BaseAsset == _baseAsset)
                .OrderBy(x => x.Pool).ThenBy(x => x.Group).ThenBy(x => x.Id)
                .ToList();

            openOrdersBindingSource.Clear();
            foreach (var order in _openOrders)
                openOrdersBindingSource.Add(order);

            cbPool.Items.Clear();
            cbPool.Items.Add("New");
            var poolNums = _openOrders.Select(x => x.Pool).Distinct();
            foreach (var poolNum in poolNums)
                cbPool.Items.Add(poolNum);

            if (cbPool.Items.Contains(_pool))
                cbPool.SelectedItem = _pool;
            else
                cbPool.SelectedIndex = 0;
        }

        private void cbPool_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _pool = cbPool.SelectedItem.ToString() == "New" ? 0 : Convert.ToInt32(cbPool.SelectedItem);

                cbGroup.Items.Clear();
                cbGroup.Items.Add("New");
                var groups = _openOrders.Where(x => x.Pool == _pool).Select(x => x.Group);
                foreach (var group in groups)
                    cbGroup.Items.Add(group);

                if (cbGroup.Items.Contains(_group))
                    cbGroup.SelectedItem = _group;
                else
                    cbGroup.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _group = cbGroup.SelectedItem.ToString() == "New" ? 0 : Convert.ToInt32(cbGroup.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbQuoteAsset_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_quoteAsset == cbQuoteAsset.SelectedItem.ToString()) return;
                _quoteAsset = cbQuoteAsset.SelectedItem.ToString();
                if (_baseQty <= 0M) return;
                ValidateOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbBaseQty_Leave(object sender, EventArgs e)
        {
            try
            {
                var oldBaseQty = _baseQty;
                if (!decimal.TryParse(tbBaseQty.Text.Trim(), out _baseQty) || _baseQty <= 0M)
                {
                    _baseQty = 0M;
                    _quoteQty = 0M;
                    tbQuoteQty.Text = "";
                    if (!string.IsNullOrWhiteSpace(tbBaseQty.Text)) throw new Exception("Invalid Base Qty");
                    return;
                }
                if (_baseQty != oldBaseQty) ValidateOrder();
            }
            catch (Exception ex)
            {
                tbBaseQty.Focus();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbQuoteQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(tbQuoteQty.Text.Trim(), out _quoteQty) || _quoteQty <= 0M)
                {
                    _quoteQty = 0M;
                    if (!string.IsNullOrWhiteSpace(tbQuoteQty.Text)) throw new Exception("Invalid Quote Qty");
                }
            }
            catch (Exception ex)
            {
                tbQuoteQty.Focus();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidateOrder(bool setQuoteQty = true)
        {
            try
            {
                if (string.IsNullOrEmpty(_baseAsset)) throw new Exception("Missing Base Asset");

                if (_baseAsset == _quoteAsset) throw new Exception("Base and Quote Asset should be different");

                if (_binance.IsInsufficientQty(_baseAsset, _baseQty))
                    throw new Exception("Base Qty is too less for an order");

                var symbol = _baseAsset == "BTC" || _baseAsset == "USDT" ? "BTCUSDT" : _baseAsset + "BTC";
                _baseQty = _binance.GetBestPossibleLotSize(symbol, _baseQty);
                tbBaseQty.Text = _baseQty.ToString();

                var groupSum = _openOrders.Where(x => x.Pool == _pool && x.Group == _group).Sum(x => x.BaseQty);

                var otherPoolsMaxQtySum = _openOrders.Where(x => x.Pool != _pool)
                    .GroupBy(x => x.Pool).Sum(x => x.GroupBy(y => y.Group).Max(y => y.Sum(z => z.BaseQty)));

                var maxPossibleBaseQty = _baseAssetFreeBalance - otherPoolsMaxQtySum - groupSum;

                if (_baseQty > maxPossibleBaseQty)
                    throw new Exception("Maximum available Base Qty for this order is " + maxPossibleBaseQty);

                if (setQuoteQty)
                {
                    _quoteQty = _binance.GetQuote(_baseAsset, _quoteAsset, _baseQty, _accountInfo.TakerCommission);
                    tbQuoteQty.Text = _quoteQty.ToString();
                }
                else
                {
                    if (_quoteQty <= 0M) throw new Exception("Invalid Quote Qty");
                }
            }
            catch (Exception ex)
            {
                tbBaseQty.Text = "";
                _baseQty = 0M;
                tbQuoteQty.Text = "";
                _quoteQty = 0M;
                throw ex;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateOrder(setQuoteQty: false);
                _db.AddOrder(_user.Id, _baseAsset, _pool, _group, _baseQty, _quoteAsset, _quoteQty);
                tbBaseQty.Text = "";
                _baseQty = 0M;
                tbQuoteQty.Text = "";
                _quoteQty = 0M;
                RefreshOpenOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgOpenOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex != dgOpenOrders.Columns["Action"].Index) return;

                var orderId = Convert.ToInt32(dgOpenOrders.Rows[e.RowIndex].Cells[0].Value);
                _db.CancelOrder(orderId, null);
                RefreshOpenOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    _binance.Reset(txtApiKey.Text.Trim(), txtSecretKey.Text.Trim(), 30000, 9000);
                    _allAssets = _binance.AccountInfo.Balances.Select(x => x.Asset).ToList();
                }
                catch (BinanceException bex)
                {
                    if (_timer.Enabled) _timer.Stop();
                    HandleBinanceException(bex);
                    return;
                }

                if (_user == null)
                {
                    _quoteAssets = new List<string>() { "BTC", "USDT" };
                    clbQuoteAssets.Items.Clear();
                    foreach (var asset in _allAssets)
                    {
                        clbQuoteAssets.Items.Add(asset, _quoteAssets.Contains(asset));
                    }
                    MessageBox.Show("Quotes on the Dashboard are shown for the selected assets on this tab", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                var quoteAssets = new List<string>();
                foreach (var item in clbQuoteAssets.CheckedItems)
                {
                    quoteAssets.Add(item.ToString());
                }

                _user = _db.SaveUser(lblIPValue.Text, txtApiKey.Text.Trim(), txtSecretKey.Text.Trim(), quoteAssets);

                LoadDashboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelSettings_Click(object sender, EventArgs e)
        {
            try
            {
                _user = _db.GetUser(lblIPValue.Text);
                if (_user == null)
                {
                    MessageBox.Show("Provide ApiKey and SecretKey to get started!", "Unknown IP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                txtApiKey.Text = _user.ApiKey;
                txtSecretKey.Text = _user.SecretKey;
                clbQuoteAssets.Items.Clear();
                foreach (var asset in _allAssets)
                {
                    clbQuoteAssets.Items.Add(asset, _quoteAssets.Contains(asset));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleBinanceException(BinanceException bex)
        {
            var err = bex.Msg;
            if (bex.Code == -2014)
            {
                err = "Invalid Api Key";
            }
            else if (bex.Code == -1022)
            {
                err = "Invalid Secret Key";
            }
            MessageBox.Show(err, "Binance Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnRefreshOrdersHistory_Click(object sender, EventArgs e)
        {
            try
            {
                var baseAsset = cbBaseAsset2.SelectedItem.ToString();
                var status = cbOrderStatus.SelectedItem.ToString();

                var orders = _db.GetOrders(_user.Id);

                var assets = orders.Select(x => x.BaseAsset).Distinct().ToList();
                cbBaseAsset2.Items.Clear();
                cbBaseAsset2.Items.Add("All");
                foreach (var asset in assets)
                    cbBaseAsset2.Items.Add(asset);
                cbBaseAsset2.SelectedItem = baseAsset;

                if (baseAsset != "All")
                    orders = orders.Where(x => x.BaseAsset == baseAsset).ToList();
                if (status != "All")
                    orders = orders.Where(x => x.Status.ToString() == status).ToList();
                
                orders = orders.OrderBy(x => x.BaseAsset).OrderByDescending(x => x.Pool)
                    .ThenBy(x => x.Group).ThenBy(x => x.Id).ToList();

                ordersHistoryBindingSource.Clear();
                foreach (var order in orders)
                    ordersHistoryBindingSource.Add(order);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChangeBotStatus_Click(object sender, EventArgs e)
        {
            try
            {
                _user.BotStatus = btnChangeBotStatus.Text == "Start Bot" ? BotStatus.StartRequested : BotStatus.StopRequested;
               _db.UpdateBotStatus(_user.Id, _user.BotStatus);
               UpdateBotStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
