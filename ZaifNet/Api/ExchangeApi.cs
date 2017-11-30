using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Jil;
using ZaifNet.Api.Result;
using ZaifNet.Common;
using ZaifNet.Types;

namespace ZaifNet.Api
{
    public class ExchangeApi : IDisposable
    {
        private static readonly string EndPoint = "https://api.zaif.jp/tapi";

        private readonly WebClient _client = new WebClient();

        private readonly ApiKey _apiKey;
        private long _nonce;

        public ExchangeApi(ApiKey apiKey)
        {
            if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));

            _apiKey = apiKey;
            _nonce = MakeNonceFromCurrentTime();
        }

        public ExchangeApi(ApiKey apiKey, long nonce)
        {
            if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));

            _apiKey = apiKey;
            _nonce = nonce;
        }

        private static long MakeNonceFromCurrentTime()
        {
            var basetTime = new DateTime(2016, 1, 1).ToUnixTimeSeconds();
            var currentTime = DateTime.Now.ToUnixTimeSeconds();
            return currentTime - basetTime;
        }

        public async Task<TradeResult> ExecuteTradeAsync(CurrencyPair currencyPair,
            TradeType tradeType, double price, double ammount)
        {
            return await ExecuteTradeAsync(currencyPair, tradeType, price, ammount, null).ConfigureAwait(false);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="currencyPair"></param>
        /// <param name="tradeType"></param>
        /// <param name="price"></param>
        /// <param name="ammount"></param>
        /// <param name="limit"></param>
        /// <exception cref="System.Net.WebException">レスポンスがなかった際に発生します。</exception>
        /// <returns></returns>
        public async Task<TradeResult> ExecuteTradeAsync(CurrencyPair currencyPair,
            TradeType tradeType, double price, double ammount, double? limit)
        {
            var message = $"&currency_pair={currencyPair.Symbol}" +
                          $"&action={tradeType.Symbol}" +
                          $"&price={price}" +
                          $"&amount={ammount}" +
                          (limit != null ? $"&i_limit={limit.Value}" : "");
            var json = await DoCommandAsync("trade", message).ConfigureAwait(false);
            return new TradeResult(json);
        }

        /// <param name="method"></param>
        /// <param name="additionalParams"></param>
        /// <exception cref="System.Net.WebException"></exception>
        /// <returns>json</returns>
        private async Task<dynamic> DoCommandAsync(string method, string additionalParams)
        {
            Interlocked.Increment(ref _nonce);
            var query = $"nonce={_nonce}&method={method}";
            if (!string.IsNullOrEmpty(additionalParams))
            {
                query += additionalParams;
            }

            _client.Headers["Key"] = _apiKey.Key;
            _client.Headers["Sign"] = _apiKey.MakeSignedHex(query);
            var postResult = await _client.UploadStringTaskAsync($"{EndPoint}", query);
            return JSON.DeserializeDynamic(postResult);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}