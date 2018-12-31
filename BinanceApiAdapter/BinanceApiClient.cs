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
        private readonly AccountInfo _accountInfo;
        private readonly RestClient _client;
        private readonly ConcurrentDictionary<string, SymbolOrdersInfo> _orderBook;
        private string _apiKey;
        private HMACSHA256 _hmac;
        private WebSocket _wsAccountInfo;
        private long _lastAccountEventTime;

        public BinanceApiClient(string apiKey, string secretKey)
        {
            _accountInfo = new AccountInfo();
            _orderBook = new ConcurrentDictionary<string, SymbolOrdersInfo>();
            ResetApiKeys(apiKey, secretKey);
            _client = new RestClient("https://api.binance.com");
            _client.AddHandler("application/json", new RestSharpJsonSerializer());
            // request.JsonSerializer = new RestSharpJsonSerializer(); ----- use if sending JSON body
        }

        public void ResetApiKeys(string apiKey, string secretKey)
        {
            _apiKey = apiKey;
            _hmac = new HMACSHA256(Encoding.ASCII.GetBytes(secretKey));
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

        public AccountInfo GetAccountInfo()
        {
            if (CurrentServerTime() - _accountInfo.UpdateTime < 10000) return _accountInfo;
            if (_wsAccountInfo == null || !_wsAccountInfo.IsAlive) Task.Run(() => GetAccountInfoWss());
            var request = new RestRequest("/api/v3/account", Method.GET, DataFormat.Json);
            return ProcessRequest<AccountInfo>(request, SecurityType.USER_DATA);
        }

        public void GetAccountInfoWss()
        {
            try
            {
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
                    if (accountInfoWss.EventType == "outboundAccountInfo" && accountInfoWss.EventTime > _lastAccountEventTime)
                    {
                        _accountInfo.Balances = accountInfoWss.Balances;
                        _accountInfo.BuyerCommission = accountInfoWss.BuyerCommission;
                        _accountInfo.CanDeposit = accountInfoWss.CanDeposit;
                        _accountInfo.CanTrade = accountInfoWss.CanTrade;
                        _accountInfo.CanWithdraw = accountInfoWss.CanWithdraw;
                        _accountInfo.MakerCommission = accountInfoWss.MakerCommission;
                        _accountInfo.SellerCommission = accountInfoWss.SellerCommission;
                        _accountInfo.TakerCommission = accountInfoWss.TakerCommission;
                        _accountInfo.UpdateTime = accountInfoWss.UpdateTime;

                        if (accountInfoWss.EventTime - _lastAccountEventTime > 25 * 60 * 1000)
                        {
                            KeepAliveUserDataStream(listenKey);
                            _lastAccountEventTime = accountInfoWss.EventTime;
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
                        //log ex
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

        public ExchangeInfo GetExchangeInfo()
        {
            var request = new RestRequest("/api/v1/exchangeInfo", Method.GET, DataFormat.Json);
            return ProcessRequest<ExchangeInfo>(request);
        }

        public double GetPrice(string symbol)
        {
            var request = new RestRequest("/api/v3/ticker/price", Method.GET, DataFormat.Json);
            request.AddParameter("symbol", symbol);
            var symbolInfo = ProcessRequest<SymbolInfo>(request);
            return symbolInfo.Price;
        }

        public SymbolOrdersInfo GetOrders(string symbol)
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
                //log ex
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
