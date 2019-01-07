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

namespace UI
{
    public partial class MainForm : Form
    {
        private readonly object _quoteUpdateLock;
        private readonly System.Windows.Forms.Timer _timer;
        private readonly BackgroundWorker _bgwRefreshQuotes;
        private readonly Repository _db;
        private BinanceApiClient _binance;
        private List<string> _allAssets;
        private List<string> _quoteAssets;
        private User _user;

        public MainForm()
        {
            InitializeComponent();

            _quoteUpdateLock = new object();

            _bgwRefreshQuotes = new BackgroundWorker {WorkerSupportsCancellation = true};
            _bgwRefreshQuotes.DoWork += RefreshQuotes;
            _bgwRefreshQuotes.RunWorkerCompleted += RefreshQuotesCompleted;

            _timer = new System.Windows.Forms.Timer
            {
                Interval = 10000
            };
            _timer.Tick += TimerOnElapsed;

            _db = new Repository();
            _binance = new BinanceApiClient("", "");
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
                _binance.ResetApiKeys(_user.ApiKey, _user.SecretKey);

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

        private void RefreshQuotes(object sender, DoWorkEventArgs e)
        {
            _allAssets = _binance.AccountInfo.Balances.Select(x => x.Asset).ToList();
            _quoteAssets = _db.GetQuoteAssets(_user.Id).Select(x => x.Asset).Where(y => _allAssets.Any(z => z == y)).ToList();

            var dtBalance = new DataTable("Balances");
            dtBalance.Columns.Add("Coin");
            dtBalance.Columns.Add("Balance");
            foreach (var asset in _quoteAssets)
            {
                dtBalance.Columns.Add(asset);
            }

            foreach (var balance in _binance.AccountInfo.Balances.Where(x => x.Free > 0))
            {
                if (_binance.IsInsufficientFreeBalance(balance)) continue;

                var newRow = dtBalance.Rows.Add();
                newRow["Coin"] = balance.Asset;
                newRow["Balance"] = balance.Free;
                Parallel.ForEach(_quoteAssets, (quoteAsset) =>
                {
                    var quoteQty = _binance.GetQuote(balance.Asset, quoteAsset, balance.Free);
                    lock (_quoteUpdateLock)
                    {
                        newRow[quoteAsset] = quoteQty;
                    }
                });
            }

            e.Result = dtBalance;
        }

        private void RefreshQuotesCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                    _binance.ResetApiKeys(txtApiKey.Text.Trim(), txtSecretKey.Text.Trim());
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
