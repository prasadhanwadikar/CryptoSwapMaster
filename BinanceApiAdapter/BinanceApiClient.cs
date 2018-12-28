using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using BinanceApiAdapter.Enums;
using BinanceApiAdapter.Types;
using Newtonsoft.Json.Converters;
using RestSharp;
using RestSharp.Deserializers;

namespace BinanceApiAdapter
{
    public class BinanceApiClient
    {
        private readonly RestClient _client;
        private readonly NewtonsoftJsonSerializer _newtonsoftJsonSerializer;
        private readonly string _apiKey;
        private readonly HMACSHA256 _hmac;

        public BinanceApiClient(string apiKey, string secretKey)
        {
            _apiKey = apiKey;
            _hmac = new HMACSHA256(Encoding.ASCII.GetBytes(secretKey));
            _newtonsoftJsonSerializer = new NewtonsoftJsonSerializer();
            _client = new RestClient("https://api.binance.com");
            _client.AddHandler("application/json", _newtonsoftJsonSerializer);
            // request.JsonSerializer = _newtonsoftJsonSerializer; ----- use if sending JSON body
        }

        private void AddApiKeyTimestampAndSignature(IRestRequest request)
        {
            //var timestamp = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
            var serverTimeRequest = new RestRequest("/api/v1/time", Method.GET, DataFormat.Json);
            var serverTime = ProcessRequest<ServerTime>(serverTimeRequest);
            request.AddParameter("timestamp", serverTime.Time);
            var queryString = string.Join("&", request.Parameters.Select(x => x.Name + '=' + x.Value));
            var hash = _hmac.ComputeHash(Encoding.ASCII.GetBytes(queryString));
            var signature = BitConverter.ToString(hash).Replace("-", "");
            request.AddParameter("signature", signature);
            request.AddHeader("X-MBX-APIKEY", _apiKey);
        }

        private T ProcessRequest<T>(IRestRequest request)
        {
            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorResponse = _client.Deserialize<ErrorInfo>(response);
                throw new BinanceException(errorResponse.Data.Code, errorResponse.Data.Msg);
            }

            var deserializedResponse = _client.Deserialize<T>(response);
            return deserializedResponse.Data;
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
            var request = new RestRequest("/api/v1/depth", Method.GET, DataFormat.Json);
            request.AddParameter("symbol", symbol);
            return ProcessRequest<SymbolOrdersInfo>(request);
        }

        public AccountInfo GetAccountInfo()
        {
            var request = new RestRequest("/api/v3/account", Method.GET, DataFormat.Json);
            AddApiKeyTimestampAndSignature(request);
            return ProcessRequest<AccountInfo>(request);
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
            AddApiKeyTimestampAndSignature(request);
            return ProcessRequest<OrderInfo>(request);
        }

        public OrderInfo QueryOrder(SymbolInfo symbol, string clientOrderId)
        {
            var request = new RestRequest("/api/v3/order", Method.GET, DataFormat.Json);
            request.AddParameter("symbol", symbol.Symbol);
            request.AddParameter("origClientOrderId", clientOrderId);
            AddApiKeyTimestampAndSignature(request);
            return ProcessRequest<OrderInfo>(request);
        }

        public OrderInfo CancelOrder(SymbolInfo symbol, string clientOrderId)
        {
            var request = new RestRequest("/api/v3/order", Method.DELETE, DataFormat.Json);
            request.AddParameter("symbol", symbol.Symbol);
            request.AddParameter("origClientOrderId", clientOrderId);
            AddApiKeyTimestampAndSignature(request);
            return ProcessRequest<OrderInfo>(request);
        }

        public List<OrderInfo> OpenOrders(SymbolInfo symbol)
        {
            var request = new RestRequest("/api/v3/openOrders", Method.GET, DataFormat.Json);
            request.AddParameter("symbol", symbol.Symbol);
            AddApiKeyTimestampAndSignature(request);
            return ProcessRequest<List<OrderInfo>>(request);
        }
    }
}
