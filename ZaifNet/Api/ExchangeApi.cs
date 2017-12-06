using System;
using System.Net.Http;
using System.Text;
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

        private readonly HttpClient _client = new HttpClient();

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

        // =============================================================================================================
        // API
        // =============================================================================================================

        public async Task<ExchangeResult<CancelResult>> ExecuteCancelAsync(long orderId, CurrencyPair currencyPair = null, bool isToken = false)
        {
            var message = $"&order_id={orderId}" +
                          (currencyPair != null ? $"&currency_pair = {currencyPair}" : "") +
                          $"&is_token={isToken.ToString().ToLower()}";
            var json = await DoCommandAsync("cancel_order", message).ConfigureAwait(false);
            return new ExchangeResult<CancelResult>(json);
        }
        
        public async Task<ExchangeResult<GetInfo2Result>> QueryGetInfo2Async()
        {
            var json = await DoCommandAsync("get_info2", string.Empty).ConfigureAwait(false);
            return new ExchangeResult<GetInfo2Result>(json);
        }

        public async Task<ExchangeResult<TradeResult>> ExecuteTradeAsync(CurrencyPair currencyPair,
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
        public async Task<ExchangeResult<TradeResult>> ExecuteTradeAsync(CurrencyPair currencyPair,
            TradeType tradeType, double price, double ammount, double? limit)
        {
            var message = $"&currency_pair={currencyPair.Symbol}" +
                          $"&action={tradeType.Symbol}" +
                          $"&price={price}" +
                          $"&amount={ammount:F4}" +
                          (limit != null ? $"&i_limit={limit.Value}" : "");
            var json = await DoCommandAsync("trade", message).ConfigureAwait(false);
            return new ExchangeResult<TradeResult>(json);
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
            var content = new StringContent(query);

            _client.DefaultRequestHeaders.Add("Key", _apiKey.Key);
            _client.DefaultRequestHeaders.Add("Sign", _apiKey.MakeSignedHex(query));
            var postResult = await _client.PostAsync($"{EndPoint}", content);
//            _client.Headers["Key"] = _apiKey.Key;
//            _client.Headers["Sign"] = _apiKey.MakeSignedHex(query);
//            var postResult = await _client.UploadStringTaskAsync($"{EndPoint}", query);
            return JSON.DeserializeDynamic(await postResult.Content.ReadAsStringAsync());
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}