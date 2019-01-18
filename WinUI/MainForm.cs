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
using BinanceApiAdapter;
using BinanceApiAdapter.Enums;
using BinanceApiAdapter.Types;
using Data;
using Data.Entities;
using Data.Enums;

namespace UI
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

        private List<int> _lastOpenOrdersIds;
        private string _baseAsset = string.Empty;
        private double _baseAssetFreeBalance = 0.0;
        private int _pool = 0;
        private int _group = 0;
        private string _quoteAsset = string.Empty;
        private double _baseQty = 0.0;
        private double _quoteQty = 0.0;

        public MainForm()
        {
            InitializeComponent();

            _quoteUpdateLock = new object();
            _quoteAssets = new List<string>();
            _lastOpenOrdersIds = new List<int>();

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
                lblIPValue.Text = new WebClient().DownloadString("https://ipinfo.io/ip").Trim();
                _user = _db.GetUser(lblIPValue.Text);
                if (_user == null)
                {
                    tcSections.SelectedTab = tpSettings;
                    MessageBox.Show("Provide ApiKey and SecretKey to get started!", "Unknown IP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                txtApiKey.Text = _user.ApiKey;
                txtSecretKey.Text = _user.SecretKey;
                _binance.Reset(_user.ApiKey, _user.SecretKey, 30000, 9000);

                LoadDashboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDashboard()
        {
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
            _accountInfo = _binance.AccountInfo;
            _allAssets = _accountInfo.Balances.Select(x => x.Asset).ToList();
            _quoteAssets = _db.GetQuoteAssets(_user.Id).Select(x => x.Asset).Where(y => _allAssets.Any(z => z == y)).ToList();
            _openOrders = _db.GetOrders(_user.Id, OrderStatus.Open);

            var dtBalance = new DataTable("Balances");
            dtBalance.Columns.Add("Coin");
            dtBalance.Columns.Add("Balance");
            foreach (var asset in _quoteAssets)
            {
                dtBalance.Columns.Add(asset);
            }

            foreach (var balance in _accountInfo.Balances.Where(x => x.Free > 0))
            {
                if (_binance.IsInsufficientQty(balance.Asset, balance.Free)) continue;
                if (string.IsNullOrEmpty(_baseAsset))
                {
                    _baseAsset = balance.Asset;
                    _baseAssetFreeBalance = balance.Free;
                }

                var newRow = dtBalance.Rows.Add();
                newRow["Coin"] = balance.Asset;
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

            e.Result = dtBalance;
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

                if (clbQuoteAssets.Items.Count != _allAssets.Count)
                {
                    clbQuoteAssets.Items.Clear();
                    foreach (var asset in _allAssets)
                    {
                        clbQuoteAssets.Items.Add(asset, _quoteAssets.Contains(asset));
                    }
                }

                dgBalance.DataSource = e.Result;

                RenderOpenOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderOpenOrders()
        {
            if (cbPool.Items.Count == 0)
            {
                cbPool.Items.Add("New");
                cbPool.SelectedIndex = 0;
            }

            var existingQuoteAssets = cbQuoteAsset.Items.OfType<string>();
            if (existingQuoteAssets.Except(_quoteAssets).Any() || _quoteAssets.Except(existingQuoteAssets).Any())
            {
                cbQuoteAsset.Items.Clear();
                foreach (var quoteAsset in _quoteAssets)
                {
                    cbQuoteAsset.Items.Add(quoteAsset);
                }

                cbQuoteAsset.SelectedIndex = 0;
            }

            var openOrdersIds = _openOrders.Where(x => x.BaseAsset == _baseAsset).Select(x => x.Id);
            if (!_lastOpenOrdersIds.Except(openOrdersIds).Any() && !openOrdersIds.Except(_lastOpenOrdersIds).Any()) return;

            _lastOpenOrdersIds.Clear();
            tableOpenOrders.RowCount = 2;
            tableOpenOrders.RowCount++;

            var poolOrdersGroups = _openOrders.Where(x => x.BaseAsset == _baseAsset).GroupBy(x => x.Pool);
            foreach (var poolOrdersGroup in poolOrdersGroups)
            {
                var poolRow = tableOpenOrders.RowCount - 1;
                var groupOrdersGroups = poolOrdersGroup.GroupBy(x => x.Group);
                foreach (var groupOrdersGroup in groupOrdersGroups)
                {
                    var groupRow = tableOpenOrders.RowCount - 1;
                    foreach (var order in groupOrdersGroup)
                    {
                        var orderRow = tableOpenOrders.RowCount - 1;
                        tableOpenOrders.Controls.Add(GetLabel(order.QuoteAsset), 2, orderRow);
                        tableOpenOrders.Controls.Add(GetLabel(order.BaseQty.ToString()), 3, orderRow);
                        tableOpenOrders.Controls.Add(GetLabel(order.ExpectedQuoteQty.ToString()), 4, orderRow);
                        tableOpenOrders.Controls.Add(GetButton("-", order.Id, removeOrderClick), 5, orderRow);
                        tableOpenOrders.RowCount++;
                        _lastOpenOrdersIds.Add(order.Id);
                    }
                    var groupLabel = (Control)GetLabel(groupOrdersGroup.Key.ToString());
                    tableOpenOrders.Controls.Add(groupLabel, 1, groupRow);
                    tableOpenOrders.SetRowSpan(groupLabel, groupOrdersGroup.Count());
                }
                var poolLabel = (Control)GetLabel(poolOrdersGroup.Key.ToString());
                tableOpenOrders.Controls.Add(poolLabel, 0, poolRow);
                tableOpenOrders.SetRowSpan(poolLabel, poolOrdersGroup.Count());
            }

            var poolNums = poolOrdersGroups.Select(x => x.Key);
            var existingPoolNums = cbPool.Items.OfType<int>();
            if (poolNums.Except(existingPoolNums).Any() || existingPoolNums.Except(poolNums).Any())
            {
                cbPool.Items.Clear();
                cbPool.Items.Add("New");
                foreach (var poolNum in poolNums)
                {
                    cbPool.Items.Add(poolNum);
                }

                cbPool.SelectedIndex = 0;
            }
        }

        private void cbPool_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _pool = cbPool.SelectedItem.ToString() == "New" ? 0 : Convert.ToInt32(cbPool.SelectedItem);
                cbGroup.Items.Clear();
                cbGroup.Items.Add("New");
                var groups = _openOrders.Where(x => x.BaseAsset == _baseAsset && x.Pool == _pool).Select(x => x.Group);
                foreach (var group in groups)
                {
                    cbGroup.Items.Add(group);
                }

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
                _quoteAsset = cbQuoteAsset.SelectedItem.ToString();
                if (string.IsNullOrWhiteSpace(tbBaseQty.Text)) return;
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
                if (string.IsNullOrWhiteSpace(tbBaseQty.Text) || tbBaseQty.Text == _baseQty.ToString()) return;
                ValidateOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbQuoteQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbQuoteQty.Text) || tbQuoteQty.Text == _quoteQty.ToString()) return;
                ValidateOrder(setQuoteQty: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidateOrder(bool setQuoteQty = true)
        {
            if (!double.TryParse(tbBaseQty.Text.Trim(), out _baseQty) || _baseQty < 0.0)
                throw new Exception("Invalid Base Qty");

            if (_binance.IsInsufficientQty(_baseAsset, _baseQty))
                throw new Exception("Base Qty too less for an order");
            
            var groupSum = _openOrders.Where(x => x.BaseAsset == _baseAsset && x.Pool == _pool && x.Group == _group)
                    .Sum(x => x.BaseQty);

            var otherPoolsMaxQtySum = _openOrders.Where(x => x.BaseAsset == _baseAsset && x.Pool != _pool)
                    .GroupBy(x => x.Pool).Sum(x => x.GroupBy(y => y.Group).Max(y => y.Sum(z => z.BaseQty)));

            var maxPossibleBaseQty = _baseAssetFreeBalance - otherPoolsMaxQtySum - groupSum;

            if (_baseQty > maxPossibleBaseQty)
                throw new Exception("Maximum available Base Qty for this order is " + maxPossibleBaseQty);

            if (setQuoteQty)
            {
                tbQuoteQty.Text = _binance.GetQuote(_baseAsset, _quoteAsset, _baseQty, _accountInfo.TakerCommission).ToString();
            }
            else
            {
                if (!double.TryParse(tbQuoteQty.Text.Trim(), out _quoteQty) || _quoteQty < 0.0)
                    throw new Exception("Invalid Quote Qty");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateOrder(setQuoteQty: false);
                _db.AddOrder(_user.Id, _baseAsset, _pool, _group, _baseQty, _quoteAsset, _quoteQty);
                _openOrders = _db.GetOrders(_user.Id, OrderStatus.Open);
                RenderOpenOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeOrderClick(object sender, EventArgs e)
        {
            try
            {
                var button = (Control)sender;
                var orderId = Convert.ToInt32(button.Tag);
                _db.CancelOrder(orderId, "Cancelled by you");
                _openOrders = _db.GetOrders(_user.Id, OrderStatus.Open);
                RenderOpenOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Label GetLabel(string text)
        {
            return new Label
            {
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.None
            };
        }

        private Button GetButton(string label, int tag, Action<object, EventArgs> handler)
        {
            var button = new Button
            {
                Text = label,
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.None,
                Tag = tag
            };

            button.Click += new EventHandler(handler);

            return button;
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
    }
}
