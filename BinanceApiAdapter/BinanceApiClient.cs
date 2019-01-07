using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
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
        private volatile AccountInfo _accountInfo;
        private WebSocket _wsAccountInfo;

        public ExchangeInfo ExchangeInfo
        {
            get
            {
                //todo: implement timed caching
                var request = new RestRequest("/api/v1/exchangeInfo", Method.GET, DataFormat.Json);
                return ProcessRequest<ExchangeInfo>(request);
            }
        }

        public AccountInfo AccountInfo
        {
            get
            {
                //todo: fix and test GetAccountInfoWss()
                if (_wsAccountInfo == null || !_wsAccountInfo.IsAlive) Task.Run(() => GetAccountInfoWss());
                if (_accountInfo != null && _wsAccountInfo != null && _wsAccountInfo.IsAlive) return _accountInfo;
                var request = new RestRequest("/api/v3/account", Method.GET, DataFormat.Json);
                return ProcessRequest<AccountInfo>(request, SecurityType.USER_DATA);
            }
        }

        public BinanceApiClient(string apiKey, string secretKey)
        {
            _orderBook = new ConcurrentDictionary<string, SymbolOrdersInfo>();
            ResetApiKeys(apiKey, secretKey);
            _client = new RestClient("https://api.binance.com");
            _client.AddHandler("application/json", new RestSharpJsonSerializer());
            //request.JsonSerializer = new RestSharpJsonSerializer(); ----- use if sending JSON body
        }

        public void ResetApiKeys(string apiKey, string secretKey)
        {
            _apiKey = apiKey;
            _hmac = new HMACSHA256(Encoding.ASCII.GetBytes(secretKey));
            if (_wsAccountInfo != null && _wsAccountInfo.IsAlive) _wsAccountInfo.Close(CloseStatusCode.Away, "User changed ApiKey or Secret key");
            _accountInfo = null;
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

        public bool IsInsufficientFreeBalance(Balance balance)
        {
            double quoteQty;
            double baseQty;
            var symbol = balance.Asset == "USDT" || balance.Asset == "BTC" ? "BTCUSDT" : balance.Asset + "BTC";

            if (balance.Asset == "USDT")
            {
                baseQty = balance.Free / GetPrice(symbol);
                quoteQty = balance.Free;
            }
            else
            {
                baseQty = balance.Free;
                quoteQty = balance.Free * GetPrice(symbol);
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

        public double GetQuote(string baseAsset, string quoteAsset, double baseQty)
        {
            var quoteQty = 0.0;
            var btcQty = 0.0;
            var commissionFactor = (10000 - AccountInfo.TakerCommission) / 10000;

            if (baseAsset == quoteAsset)
            {
                quoteQty = baseQty;
            }
            else if (ExchangeInfo.Symbols.Any(x => x.Symbol == baseAsset + quoteAsset))
            {
                //sell - look for bids
                var symbolInfo = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == baseAsset + quoteAsset);
                var symbolOrdersInfo = GetOrders(symbolInfo.Symbol);
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
            else if (ExchangeInfo.Symbols.Any(x => x.Symbol == quoteAsset + baseAsset))
            {
                //buy - look for asks
                var symbolInfo = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == quoteAsset + baseAsset);
                var symbolOrdersInfo = GetOrders(symbolInfo.Symbol);
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
            else if (baseAsset == "USDT")
            {
                //buy - look for asks
                var symbolInfo1 = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == "BTC" + baseAsset);
                var symbolOrdersInfo1 = GetOrders(symbolInfo1.Symbol);
                foreach (var ask in symbolOrdersInfo1.AskOrders)
                {
                    if (baseQty <= ask.Qty * ask.Price)
                    {
                        btcQty += Math.Round(baseQty / ask.Price * commissionFactor, symbolInfo1.BaseAssetPrecision);
                        break;
                    }
                    baseQty -= Math.Round(ask.Qty * ask.Price, symbolInfo1.BaseAssetPrecision);
                    btcQty += ask.Qty * commissionFactor;
                }
                //buy - look for asks
                var symbolInfo2 = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == quoteAsset + "BTC");
                var symbolOrdersInfo2 = GetOrders(symbolInfo2.Symbol);
                foreach (var ask in symbolOrdersInfo2.AskOrders)
                {
                    if (btcQty <= ask.Qty * ask.Price)
                    {
                        quoteQty += Math.Round(btcQty / ask.Price * commissionFactor, symbolInfo2.BaseAssetPrecision);
                        break;
                    }
                    btcQty -= Math.Round(ask.Qty * ask.Price, symbolInfo2.BaseAssetPrecision);
                    quoteQty += ask.Qty * commissionFactor;
                }
            }
            else if (quoteAsset == "USDT")
            {
                //sell - look for bids
                var symbolInfo1 = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == baseAsset + "BTC");
                var symbolOrdersInfo1 = GetOrders(symbolInfo1.Symbol);
                foreach (var bid in symbolOrdersInfo1.BidOrders)
                {
                    if (baseQty <= bid.Qty)
                    {
                        btcQty += Math.Round(baseQty * bid.Price * commissionFactor, symbolInfo1.QuotePrecision);
                        break;
                    }
                    baseQty -= bid.Qty;
                    btcQty += Math.Round(bid.Qty * bid.Price * commissionFactor, symbolInfo1.QuotePrecision);
                }
                //sell - look for bids
                var symbolInfo2 = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == "BTC" + quoteAsset);
                var symbolOrdersInfo2 = GetOrders(symbolInfo2.Symbol);
                foreach (var bid in symbolOrdersInfo2.BidOrders)
                {
                    if (btcQty <= bid.Qty)
                    {
                        quoteQty += Math.Round(btcQty * bid.Price * commissionFactor, symbolInfo2.QuotePrecision);
                        break;
                    }
                    btcQty -= bid.Qty;
                    quoteQty += Math.Round(bid.Qty * bid.Price * commissionFactor, symbolInfo2.QuotePrecision);
                }
            }
            else
            {
                //sell - look for bids
                var symbolInfo1 = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == baseAsset + "BTC");
                var symbolOrdersInfo1 = GetOrders(symbolInfo1.Symbol);
                foreach (var bid in symbolOrdersInfo1.BidOrders)
                {
                    if (baseQty <= bid.Qty)
                    {
                        btcQty += Math.Round(baseQty * bid.Price * commissionFactor, symbolInfo1.QuotePrecision);
                        break;
                    }
                    baseQty -= bid.Qty;
                    btcQty += Math.Round(bid.Qty * bid.Price * commissionFactor, symbolInfo1.QuotePrecision);
                }
                //buy - look for asks
                var symbolInfo2 = ExchangeInfo.Symbols.FirstOrDefault(x => x.Symbol == quoteAsset + "BTC");
                var symbolOrdersInfo2 = GetOrders(symbolInfo2.Symbol);
                foreach (var ask in symbolOrdersInfo2.AskOrders)
                {
                    if (btcQty <= ask.Qty * ask.Price)
                    {
                        quoteQty += Math.Round(btcQty / ask.Price * commissionFactor, symbolInfo2.BaseAssetPrecision);
                        break;
                    }
                    btcQty -= Math.Round(ask.Qty * ask.Price, symbolInfo2.BaseAssetPrecision);
                    quoteQty += ask.Qty * commissionFactor;
                }
            }

            return quoteQty;
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

        public OrderInfo NewOrder(SymbolInfo symbol, OrderSide side, OrderType type, TimeInForce timeInForce, double quantity, double price)
        {
            var request = new RestRequest("/api/v3/order", Method.POST, DataFormat.Json);
            request.AddParameter("symbol", symbol.Symbol);
            request.AddParameter("side", side.ToString());
            request.AddParameter("type", type.ToString());
            request.AddParameter("timeInForce", timeInForce.ToString());
            request.AddParameter("quantity", quantity);
            request.AddParameter("price", price);
            return ProcessRequest<OrderInfo>(request, SecurityType.USER_DATA);
        }

        public OrderInfo QueryOrder(SymbolInfo symbol, string clientOrderId)
        {
            var request = new RestRequest("/api/v3/order", Method.GET, DataFormat.Json);
            request.AddParameter("symbol", symbol.Symbol);
            request.AddParameter("origClientOrderId", clientOrderId);
            return ProcessRequest<OrderInfo>(request, SecurityType.USER_DATA);
        }

        public OrderInfo CancelOrder(SymbolInfo symbol, string clientOrderId)
        {
            var request = new RestRequest("/api/v3/order", Method.DELETE, DataFormat.Json);
            request.AddParameter("symbol", symbol.Symbol);
            request.AddParameter("origClientOrderId", clientOrderId);
            return ProcessRequest<OrderInfo>(request, SecurityType.USER_DATA);
        }

        public List<OrderInfo> OpenOrders(SymbolInfo symbol)
        {
            var request = new RestRequest("/api/v3/openOrders", Method.GET, DataFormat.Json);
            request.AddParameter("symbol", symbol.Symbol);
            return ProcessRequest<List<OrderInfo>>(request, SecurityType.USER_DATA);
        }
    }
}
