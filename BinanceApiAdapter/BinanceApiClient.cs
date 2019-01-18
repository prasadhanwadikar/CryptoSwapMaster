using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BinanceApiAdapter.Enums;
using BinanceApiAdapter.Types;
using Newtonsoft.Json.Converters;
using RestSharp;
using RestSharp.Deserializers;
using WebSocketSharp;

namespace BinanceApiAdapter
{
    public class BinanceApiClient
    {
        private readonly RestClient _client;
        private readonly ConcurrentDictionary<string, SymbolOrdersInfo> _orderBook;
        private string _apiKey;
        private HMACSHA256 _hmac;
        
        private ExchangeInfo _exchangeInfo;
        private object _exchangeInfoLock;
        private long _lastExchangeInfoTicks;
        private long _exchangeInfoCacheDuration;

        private AccountInfo _accountInfo;
        private object _accountInfoLock;
        private long _lastAccountInfoTicks;
        private long _accountInfoCacheDuration;
        private WebSocket _wsAccountInfo;

        public ExchangeInfo ExchangeInfo
        {
            get
            {
                lock (_exchangeInfoLock)
                {
                    if (_exchangeInfo == null || DateTime.UtcNow.Ticks - _lastExchangeInfoTicks > _exchangeInfoCacheDuration)
                    {
                        var request = new RestRequest("/api/v1/exchangeInfo", Method.GET, DataFormat.Json);
                        _exchangeInfo = ProcessRequest<ExchangeInfo>(request);
                        _lastExchangeInfoTicks = DateTime.UtcNow.Ticks;
                    }

                    return _exchangeInfo;
                }
            }
        }

        public AccountInfo AccountInfo
        {
            get
            {
                lock (_accountInfoLock)
                {
                    if (_accountInfo == null || DateTime.UtcNow.Ticks - _lastAccountInfoTicks > _accountInfoCacheDuration)
                    {
                        if (_wsAccountInfo == null || !_wsAccountInfo.IsAlive) Task.Run(() => GetAccountInfoWss());
                        var request = new RestRequest("/api/v3/account", Method.GET, DataFormat.Json);
                        _accountInfo = ProcessRequest<AccountInfo>(request, SecurityType.USER_DATA);
                        _lastAccountInfoTicks = DateTime.UtcNow.Ticks;
                    }

                    return _accountInfo;
                }
            }
        }

        public BinanceApiClient(string apiKey, string secretKey, long exchangeInfoCacheDuration, long accountInfoCacheDuration)
        {
            _exchangeInfoLock = new object();
            _accountInfoLock = new object();
            _orderBook = new ConcurrentDictionary<string, SymbolOrdersInfo>();
            _client = new RestClient("https://api.binance.com");
            _client.AddHandler("application/json", new RestSharpJsonSerializer());
            //request.JsonSerializer = new RestSharpJsonSerializer(); ----- use if sending JSON body

            Reset(apiKey, secretKey, exchangeInfoCacheDuration, accountInfoCacheDuration);
        }

        public void Reset(string apiKey, string secretKey, long exchangeInfoCacheDuration, long accountInfoCacheDuration)
        {
            _apiKey = apiKey;
            _hmac = new HMACSHA256(Encoding.ASCII.GetBytes(secretKey));
            if (_wsAccountInfo != null && _wsAccountInfo.IsAlive) _wsAccountInfo.Close(CloseStatusCode.Away, "User changed ApiKey or Secret key");
            _accountInfo = null;
            _exchangeInfoCacheDuration = exchangeInfoCacheDuration;
            _accountInfoCacheDuration = accountInfoCacheDuration;
        }

        private T ProcessRequest<T>(IRestRequest request, SecurityType securityType = SecurityType.NONE)
        {
            if (securityType == SecurityType.USER_DATA)
            {
                request.AddParameter("timestamp", CurrentServerTime());
                var queryString = string.Join("&", request.Parameters.Select(x => x.Name + '=' + x.Value));
                var hash = _hmac.ComputeHash(Encoding.ASCII.GetBytes(queryString));
                var signature = BitConverter.ToString(hash).Replace("-", "");
                request.AddParameter("signature", signature);
                request.AddHeader("X-MBX-APIKEY", _apiKey);
            }
            else if (securityType == SecurityType.USER_STREAM)
            {
                request.AddHeader("X-MBX-APIKEY", _apiKey);
            }

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorResponse = _client.Deserialize<ErrorInfo>(response);
                if (errorResponse.Data != null)
                    throw new BinanceException(errorResponse.Data.Code, errorResponse.Data.Msg);
                else
                    throw new BinanceException(-1, errorResponse.ErrorMessage);
            }

            var deserializedResponse = _client.Deserialize<T>(response);
            return deserializedResponse.Data;
        }

        private long CurrentServerTime()
        {
            var serverTimeRequest = new RestRequest("/api/v1/time", Method.GET, DataFormat.Json);
            return ProcessRequest<ServerTime>(serverTimeRequest).Time;
        }

        private void GetAccountInfoWss()
        {
            try
            {
                long lastAccountEventTime = 0;
                var listenKey = NewUserDataStream();
                var jsonSerializer = new JsonSerializer();
                _wsAccountInfo = new WebSocket("wss://stream.binance.com:9443/ws/" + listenKey);
                _wsAccountInfo.OnOpen += (s, e) =>
                {
                    KeepAliveUserDataStream(listenKey);
                };
                _wsAccountInfo.OnMessage += (s, e) =>
                {
                    var accountInfoWss = jsonSerializer.Deserialize<AccountInfoWss>(e.Data);
                    if (accountInfoWss.EventType == "outboundAccountInfo" && accountInfoWss.EventTime > lastAccountEventTime)
                    {
                        _lastAccountInfoTicks = DateTime.UtcNow.Ticks;
                        _accountInfo = new AccountInfo
                                            {
                                                Balances = accountInfoWss.Balances.Select(b => new Balance
                                                {
                                                    Asset = b.Asset,
                                                    Free = b.Free,
                                                    Locked = b.Locked
                                                }).ToList(),
                                                CanTrade = accountInfoWss.CanTrade,
                                                UpdateTime = accountInfoWss.UpdateTime,
                                                SellerCommission = accountInfoWss.SellerCommission,
                                                TakerCommission = accountInfoWss.TakerCommission,
                                                MakerCommission = accountInfoWss.MakerCommission,
                                                BuyerCommission = accountInfoWss.BuyerCommission,
                                                CanDeposit = accountInfoWss.CanDeposit,
                                                CanWithdraw = accountInfoWss.CanWithdraw
                                            };

                        if (accountInfoWss.EventTime - lastAccountEventTime > 25 * 60 * 1000)
                        {
                            KeepAliveUserDataStream(listenKey);
                            lastAccountEventTime = accountInfoWss.EventTime;
                        }
                    }
                };
                _wsAccountInfo.OnError += (s, e) =>
                {
                    if (_wsAccountInfo.IsAlive) _wsAccountInfo.Close(CloseStatusCode.ServerError, e.Message);
                };
                _wsAccountInfo.OnClose += (s, e) =>
                {
                    try
                    {
                        CloseUserDataStream(listenKey);
                    }
                    catch (Exception ex)
                    {
                        //todo: log ex
                    }
                    //log e.Reason
                };
                _wsAccountInfo.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls | SslProtocols.Ssl3;
                _wsAccountInfo.Connect();
            }
            catch (Exception ex)
            {
                //log ex
            }
        }

        private string NewUserDataStream()
        {
            var request = new RestRequest("/api/v1/userDataStream", Method.POST, DataFormat.Json);
            return ProcessRequest<UserDataStream>(request, SecurityType.USER_STREAM).ListenKey;
        }

        private void KeepAliveUserDataStream(string listenKey)
        {
            var request = new RestRequest("/api/v1/userDataStream", Method.PUT, DataFormat.Json);
            request.AddParameter("listenKey", listenKey);
            ProcessRequest<EmptyResponse>(request, SecurityType.USER_STREAM);
        }

        private void CloseUserDataStream(string listenKey)
        {
            var request = new RestRequest("/api/v1/userDataStream", Method.DELETE, DataFormat.Json);
            request.AddParameter("listenKey", listenKey);
            ProcessRequest<EmptyResponse>(request, SecurityType.USER_STREAM);
        }

        public bool IsInsufficientQty(string asset, double qty)
        {
            double quoteQty;
            double baseQty;
            var symbol = asset == "USDT" || asset == "BTC" ? "BTCUSDT" : asset + "BTC";

            if (asset == "USDT")
            {
                baseQty = qty / GetPrice(symbol);
                quoteQty = qty;
            }
            else
            {
                baseQty = qty;
                quoteQty = qty * GetPrice(symbol);
            }

            var symbolInfo = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == symbol);
            var minNotionalFilter = symbolInfo.Filters.FirstOrDefault(x => x.FilterType == FilterType.MIN_NOTIONAL);
            if (minNotionalFilter != null)
            {
                if (quoteQty < minNotionalFilter.MinNotional) return true;
            }
            var lotSizeFilter = symbolInfo.Filters.FirstOrDefault(x => x.FilterType == FilterType.LOT_SIZE);
            if (lotSizeFilter != null)
            {
                if (baseQty < lotSizeFilter.MinQty) return true;
            }

            return false;
        }

        private double GetPrice(string symbol)
        {
            var request = new RestRequest("/api/v3/ticker/price", Method.GET, DataFormat.Json);
            request.AddParameter("symbol", symbol);
            var symbolInfo = ProcessRequest<SymbolInfo>(request);
            return symbolInfo.Price;
        }

        private double GetQuote(SymbolInfo symbolInfo, BinanceOrderSide orderSide, double baseQty, double takerCommission)
        {
            var quoteQty = 0.0;
            var commissionFactor = (10000 - takerCommission) / 10000;

            var symbolOrdersInfo = GetOrders(symbolInfo.Symbol);

            if (orderSide == BinanceOrderSide.BUY)
            {
                foreach (var ask in symbolOrdersInfo.AskOrders)
                {
                    if (baseQty <= ask.Qty * ask.Price)
                    {
                        quoteQty += Math.Round(baseQty / ask.Price * commissionFactor, symbolInfo.BaseAssetPrecision);
                        break;
                    }
                    baseQty -= Math.Round(ask.Qty * ask.Price, symbolInfo.BaseAssetPrecision);
                    quoteQty += ask.Qty * commissionFactor;
                }
            }
            else
            {
                foreach (var bid in symbolOrdersInfo.BidOrders)
                {
                    if (baseQty <= bid.Qty)
                    {
                        quoteQty += Math.Round(baseQty * bid.Price * commissionFactor, symbolInfo.QuotePrecision);
                        break;
                    }
                    baseQty -= bid.Qty;
                    quoteQty += Math.Round(bid.Qty * bid.Price * commissionFactor, symbolInfo.QuotePrecision);
                }
            }

            return quoteQty;
        }

        public List<OrderInfo> BuildOrders(string baseAsset, string quoteAsset, double baseQty, double takerCommission)
        {
            var orders = new List<OrderInfo>();

            if (ExchangeInfo.Symbols.Any(x => x.Symbol == baseAsset + quoteAsset))
            {
                //sell - look for bids
                var symbol = baseAsset + quoteAsset;
                var symbolInfo = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == symbol);
                var order = new OrderInfo()
                {
                    Symbol = symbol,
                    Side = BinanceOrderSide.SELL,
                    OrigQty = baseQty,
                    CummulativeQuoteQty = GetQuote(symbolInfo, BinanceOrderSide.SELL, baseQty, takerCommission)
                };
                orders.Add(order);
            }
            else if (ExchangeInfo.Symbols.Any(x => x.Symbol == quoteAsset + baseAsset))
            {
                //buy - look for asks
                var symbol = quoteAsset + baseAsset;
                var symbolInfo = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == symbol);
                var order = new OrderInfo()
                {
                    Symbol = symbol,
                    Side = BinanceOrderSide.BUY,
                    OrigQty = baseQty,
                    CummulativeQuoteQty = GetQuote(symbolInfo, BinanceOrderSide.BUY, baseQty, takerCommission)
                };
                orders.Add(order);
            }
            else if (baseAsset == "USDT")
            {
                //buy - look for asks
                var symbol1 = "BTC" + baseAsset;
                var symbolInfo1 = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == symbol1);
                var order1 = new OrderInfo()
                {
                    Symbol = symbol1,
                    Side = BinanceOrderSide.BUY,
                    OrigQty = baseQty,
                    CummulativeQuoteQty = GetQuote(symbolInfo1, BinanceOrderSide.BUY, baseQty, takerCommission)
                };
                orders.Add(order1);

                //buy - look for asks
                var symbol2 = quoteAsset + "BTC";
                var symbolInfo2 = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == symbol2);
                var order2 = new OrderInfo()
                {
                    Symbol = symbol2,
                    Side = BinanceOrderSide.BUY,
                    OrigQty = order1.CummulativeQuoteQty,
                    CummulativeQuoteQty = GetQuote(symbolInfo2, BinanceOrderSide.BUY, order1.CummulativeQuoteQty, takerCommission)
                };
                orders.Add(order2);
            }
            else if (quoteAsset == "USDT")
            {
                //sell - look for bids
                var symbol1 = baseAsset + "BTC";
                var symbolInfo1 = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == symbol1);
                var order1 = new OrderInfo()
                {
                    Symbol = symbol1,
                    Side = BinanceOrderSide.SELL,
                    OrigQty = baseQty,
                    CummulativeQuoteQty = GetQuote(symbolInfo1, BinanceOrderSide.SELL, baseQty, takerCommission)
                };
                orders.Add(order1);

                //sell - look for bids
                var symbol2 = "BTC" + quoteAsset;
                var symbolInfo2 = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == symbol2);
                var order2 = new OrderInfo()
                {
                    Symbol = symbol2,
                    Side = BinanceOrderSide.SELL,
                    OrigQty = order1.CummulativeQuoteQty,
                    CummulativeQuoteQty = GetQuote(symbolInfo2, BinanceOrderSide.SELL, order1.CummulativeQuoteQty, takerCommission)
                };
                orders.Add(order2);
            }
            else
            {
                //sell - look for bids
                var symbol1 = baseAsset + "BTC";
                var symbolInfo1 = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == symbol1);
                var order1 = new OrderInfo()
                {
                    Symbol = symbol1,
                    Side = BinanceOrderSide.SELL,
                    OrigQty = baseQty,
                    CummulativeQuoteQty = GetQuote(symbolInfo1, BinanceOrderSide.SELL, baseQty, takerCommission)
                };
                orders.Add(order1);

                //buy - look for asks
                var symbol2 = quoteAsset + "BTC";
                var symbolInfo2 = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == symbol2);
                var order2 = new OrderInfo()
                {
                    Symbol = symbol2,
                    Side = BinanceOrderSide.BUY,
                    OrigQty = order1.CummulativeQuoteQty,
                    CummulativeQuoteQty = GetQuote(symbolInfo2, BinanceOrderSide.BUY, order1.CummulativeQuoteQty, takerCommission)
                };
                orders.Add(order2);
            }

            return orders;
        }

        public double GetQuote(string baseAsset, string quoteAsset, double baseQty, double takerCommission)
        {
            if (baseAsset == quoteAsset) return baseQty;
            var orders = BuildOrders(baseAsset, quoteAsset, baseQty, takerCommission);
            return orders.Last().CummulativeQuoteQty;
        }

        private SymbolOrdersInfo GetOrders(string symbol)
        {
            if (_orderBook.ContainsKey(symbol)) return _orderBook[symbol];
            Task.Run(() => GetOrdersWss(symbol));
            var request = new RestRequest("/api/v1/depth", Method.GET, DataFormat.Json);
            request.AddParameter("symbol", symbol);
            return ProcessRequest<SymbolOrdersInfo>(request);
        }

        private void GetOrdersWss(string symbol)
        {
            try
            {
                var jsonSerializer = new JsonSerializer();
                var ws = new WebSocket("wss://stream.binance.com:9443/ws/" + symbol.ToLower() + "@depth20")
                {
                    EmitOnPing = true
                };
                ws.OnOpen += (s, e) =>
                {

                };
                ws.OnMessage += (s, e) =>
                {
                    if (e.IsPing)
                    {
                        ws.Ping();
                        return;
                    }
                    _orderBook[symbol] = jsonSerializer.Deserialize<SymbolOrdersInfo>(e.Data);
                };
                ws.OnError += (s, e) =>
                {
                    if (ws.IsAlive) ws.Close(CloseStatusCode.ServerError, e.Message);
                };
                ws.OnClose += (s, e) =>
                {
                    _orderBook.TryRemove(symbol, out SymbolOrdersInfo value);
                    //log the e.Reason
                };
                ws.Connect();
            }
            catch (Exception ex)
            {
                //todo: log ex
            }
        }

        public OrderInfo NewMarketOrder(string symbol, string side, double quantity)
        {
            var request = new RestRequest("/api/v3/order", Method.POST, DataFormat.Json);
            request.AddParameter("symbol", symbol);
            request.AddParameter("side", side);
            request.AddParameter("type", BinanceOrderType.MARKET.ToString());
            request.AddParameter("quantity", quantity);
            return ProcessRequest<OrderInfo>(request, SecurityType.USER_DATA);
        }

        public OrderInfo QueryOrder(string symbol, string clientOrderId)
        {
            var request = new RestRequest("/api/v3/order", Method.GET, DataFormat.Json);
            request.AddParameter("symbol", symbol);
            request.AddParameter("origClientOrderId", clientOrderId);
            return ProcessRequest<OrderInfo>(request, SecurityType.USER_DATA);
        }

        public OrderInfo CancelOrder(string symbol, string clientOrderId)
        {
            var request = new RestRequest("/api/v3/order", Method.DELETE, DataFormat.Json);
            request.AddParameter("symbol", symbol);
            request.AddParameter("origClientOrderId", clientOrderId);
            return ProcessRequest<OrderInfo>(request, SecurityType.USER_DATA);
        }

        public List<OrderInfo> OpenOrders(string symbol)
        {
            var request = new RestRequest("/api/v3/openOrders", Method.GET, DataFormat.Json);
            request.AddParameter("symbol", symbol);
            return ProcessRequest<List<OrderInfo>>(request, SecurityType.USER_DATA);
        }
    }
}
