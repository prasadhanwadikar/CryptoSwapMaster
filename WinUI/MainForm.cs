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
        private readonly System.Windows.Forms.Timer _timer;
        private readonly BackgroundWorker _bgwRefreshQuotes;
        private readonly Repository _db;
        private BinanceApiClient _binance;
        private ExchangeInfo _exchangeInfo;
        private AccountInfo _accountInfo;
        private List<string> _allAssets;
        private List<string> _quoteAssets;
        private User _user;

        public MainForm()
        {
            InitializeComponent();
            
            _bgwRefreshQuotes = new BackgroundWorker {WorkerSupportsCancellation = true};
            _bgwRefreshQuotes.DoWork += RefreshQuotes;
            _bgwRefreshQuotes.RunWorkerCompleted += RefreshQuotesCompleted;

            _timer = new System.Windows.Forms.Timer
            {
                Interval = 15000
            };
            _timer.Tick += TimerOnElapsed;

            _db = new Repository();
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
                _binance = new BinanceApiClient(_user.ApiKey, _user.SecretKey);
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
            _exchangeInfo = _binance.GetExchangeInfo();
            _accountInfo = _binance.GetAccountInfo();
            _allAssets = _accountInfo.Balances.Select(x => x.Asset).ToList();
            _quoteAssets = _db.GetQuoteAssets(_user.Id).Select(x => x.Asset).Where(y => _allAssets.Any(z => z == y)).ToList();

            var commissionFactor = (10000 - _accountInfo.TakerCommission) / 10000;
            var dtBalance = new DataTable("Balances");
            dtBalance.Columns.Add("Coin");
            dtBalance.Columns.Add("Balance");
            foreach (var asset in _quoteAssets)
            {
                dtBalance.Columns.Add(asset);
            }

            foreach (var baseAssetBalance in _accountInfo.Balances.Where(x => x.Free > 0))
            {
                SymbolInfo baseBtcSymbolInfo;
                double freeBaseAssetQuoteValue;
                double freeBaseAssetQty;

                if (baseAssetBalance.Asset == "USDT" || baseAssetBalance.Asset == "BTC")
                    baseBtcSymbolInfo = _exchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == "BTCUSDT");
                else
                    baseBtcSymbolInfo = _exchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == baseAssetBalance.Asset + "BTC");

                if (baseAssetBalance.Asset == "USDT")
                {
                    freeBaseAssetQuoteValue = baseAssetBalance.Free;
                    freeBaseAssetQty = baseAssetBalance.Free / _binance.GetPrice(baseBtcSymbolInfo.Symbol);
                }
                else
                {
                    freeBaseAssetQuoteValue = baseAssetBalance.Free * _binance.GetPrice(baseBtcSymbolInfo.Symbol);
                    freeBaseAssetQty = baseAssetBalance.Free;
                }

                var minNotionalFilter = baseBtcSymbolInfo.Filters.FirstOrDefault(x => x.FilterType == FilterType.MIN_NOTIONAL);
                if (minNotionalFilter != null)
                {
                    if (freeBaseAssetQuoteValue < minNotionalFilter.MinNotional) continue;
                }

                var lotSizeFilter = baseBtcSymbolInfo.Filters.FirstOrDefault(x => x.FilterType == FilterType.LOT_SIZE);
                if (lotSizeFilter != null)
                {
                    if (freeBaseAssetQty < lotSizeFilter.MinQty) continue;
                }

                var newRow = dtBalance.Rows.Add();
                newRow["Coin"] = baseAssetBalance.Asset;
                newRow["Balance"] = baseAssetBalance.Free;

                //foreach (var quoteAsset in _quoteAssets)
                Parallel.ForEach(_quoteAssets, (quoteAsset) =>
                {
                    var baseValue = baseAssetBalance.Free;
                    var quoteValue = 0.0;
                    var btcValue = 0.0;

                    if (baseAssetBalance.Asset == quoteAsset)
                    {
                        quoteValue = baseAssetBalance.Free;
                    }
                    else if (_exchangeInfo.Symbols.Any(x => x.Symbol == baseAssetBalance.Asset + quoteAsset))
                    {
                        //sell - look for bids
                        var symbolInfo = _exchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == baseAssetBalance.Asset + quoteAsset);
                        var symbolOrdersInfo = _binance.GetOrders(symbolInfo.Symbol);
                        foreach (var bid in symbolOrdersInfo.BidOrders)
                        {
                            if (baseValue <= bid.Qty)
                            {
                                quoteValue += Math.Round(baseValue * bid.Price * commissionFactor, symbolInfo.QuotePrecision);
                                break;
                            }
                            baseValue -= bid.Qty;
                            quoteValue += Math.Round(bid.Qty * bid.Price * commissionFactor, symbolInfo.QuotePrecision);
                        }
                    }
                    else if (_exchangeInfo.Symbols.Any(x => x.Symbol == quoteAsset + baseAssetBalance.Asset))
                    {
                        //buy - look for asks
                        var symbolInfo = _exchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == quoteAsset + baseAssetBalance.Asset);
                        var symbolOrdersInfo = _binance.GetOrders(symbolInfo.Symbol);
                        foreach (var ask in symbolOrdersInfo.AskOrders)
                        {
                            if (baseValue <= ask.Qty * ask.Price)
                            {
                                quoteValue += Math.Round(baseValue / ask.Price * commissionFactor, symbolInfo.BaseAssetPrecision);
                                break;
                            }
                            baseValue -= Math.Round(ask.Qty * ask.Price, symbolInfo.BaseAssetPrecision);
                            quoteValue += ask.Qty * commissionFactor;
                        }
                    }
                    else if (baseAssetBalance.Asset == "USDT")
                    {
                        //buy - look for asks
                        var symbolInfo1 = _exchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == "BTC" + baseAssetBalance.Asset);
                        var symbolOrdersInfo1 = _binance.GetOrders(symbolInfo1.Symbol);
                        foreach (var ask in symbolOrdersInfo1.AskOrders)
                        {
                            if (baseValue <= ask.Qty * ask.Price)
                            {
                                btcValue += Math.Round(baseValue / ask.Price * commissionFactor, symbolInfo1.BaseAssetPrecision);
                                break;
                            }
                            baseValue -= Math.Round(ask.Qty * ask.Price, symbolInfo1.BaseAssetPrecision);
                            btcValue += ask.Qty * commissionFactor;
                        }
                        //buy - look for asks
                        var symbolInfo2 = _exchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == quoteAsset + "BTC");
                        var symbolOrdersInfo2 = _binance.GetOrders(symbolInfo2.Symbol);
                        foreach (var ask in symbolOrdersInfo2.AskOrders)
                        {
                            if (btcValue <= ask.Qty * ask.Price)
                            {
                                quoteValue += Math.Round(btcValue / ask.Price * commissionFactor, symbolInfo2.BaseAssetPrecision);
                                break;
                            }
                            btcValue -= Math.Round(ask.Qty * ask.Price, symbolInfo2.BaseAssetPrecision);
                            quoteValue += ask.Qty * commissionFactor;
                        }
                    }
                    else if (quoteAsset == "USDT")
                    {
                        //sell - look for bids
                        var symbolInfo1 = _exchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == baseAssetBalance.Asset + "BTC");
                        var symbolOrdersInfo1 = _binance.GetOrders(symbolInfo1.Symbol);
                        foreach (var bid in symbolOrdersInfo1.BidOrders)
                        {
                            if (baseValue <= bid.Qty)
                            {
                                btcValue += Math.Round(baseValue * bid.Price * commissionFactor, symbolInfo1.QuotePrecision);
                                break;
                            }
                            baseValue -= bid.Qty;
                            btcValue += Math.Round(bid.Qty * bid.Price * commissionFactor, symbolInfo1.QuotePrecision);
                        }
                        //sell - look for bids
                        var symbolInfo2 = _exchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == "BTC" + quoteAsset);
                        var symbolOrdersInfo2 = _binance.GetOrders(symbolInfo2.Symbol);
                        foreach (var bid in symbolOrdersInfo2.BidOrders)
                        {
                            if (btcValue <= bid.Qty)
                            {
                                quoteValue += Math.Round(btcValue * bid.Price * commissionFactor, symbolInfo2.QuotePrecision);
                                break;
                            }
                            btcValue -= bid.Qty;
                            quoteValue += Math.Round(bid.Qty * bid.Price * commissionFactor, symbolInfo2.QuotePrecision);
                        }
                    }
                    else
                    {
                        //sell - look for bids
                        var symbolInfo1 = _exchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == baseAssetBalance.Asset + "BTC");
                        var symbolOrdersInfo1 = _binance.GetOrders(symbolInfo1.Symbol);
                        foreach (var bid in symbolOrdersInfo1.BidOrders)
                        {
                            if (baseValue <= bid.Qty)
                            {
                                btcValue += Math.Round(baseValue * bid.Price * commissionFactor, symbolInfo1.QuotePrecision);
                                break;
                            }
                            baseValue -= bid.Qty;
                            btcValue += Math.Round(bid.Qty * bid.Price * commissionFactor, symbolInfo1.QuotePrecision);
                        }
                        //buy - look for asks
                        var symbolInfo2 = _exchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == quoteAsset + "BTC");
                        var symbolOrdersInfo2 = _binance.GetOrders(symbolInfo2.Symbol);
                        foreach (var ask in symbolOrdersInfo2.AskOrders)
                        {
                            if (btcValue <= ask.Qty * ask.Price)
                            {
                                quoteValue += Math.Round(btcValue / ask.Price * commissionFactor, symbolInfo2.BaseAssetPrecision);
                                break;
                            }
                            btcValue -= Math.Round(ask.Qty * ask.Price, symbolInfo2.BaseAssetPrecision);
                            quoteValue += ask.Qty * commissionFactor;
                        }
                    }

                    newRow[quoteAsset] = quoteValue;
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
                    _timer.Stop();
                    var err = e.Error.GetType() == typeof(BinanceException) ? ((BinanceException)e.Error).Msg : e.Error.Message;
                    MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
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
                    _binance = new BinanceApiClient(txtApiKey.Text.Trim(), txtSecretKey.Text.Trim());
                    _exchangeInfo = _binance.GetExchangeInfo();
                    _accountInfo = _binance.GetAccountInfo();
                    _allAssets = _accountInfo.Balances.Select(x => x.Asset).ToList();
                }
                catch (BinanceException bex)
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
                    MessageBox.Show(err, "Error connecting with Binance account", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                var selectedAssets = new List<string>();
                foreach (var item in clbQuoteAssets.CheckedItems)
                {
                    selectedAssets.Add(item.ToString());
                }

                _user = _db.SaveUser(lblIPValue.Text, txtApiKey.Text.Trim(), txtSecretKey.Text.Trim(), selectedAssets);
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
    }
}
