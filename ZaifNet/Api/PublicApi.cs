using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Jil;
using ZaifNet.Api.Result;
using ZaifNet.Types;

namespace ZaifNet.Api
{
    /// <summary>
    /// Zaif 公開情報 API のラッパークラスです。
    /// https://corp.zaif.jp/api-docs/
    /// </summary>
    public class PublicApi : IDisposable
    {
        private static readonly string Endpoint = "https://api.zaif.jp/api/1";

        private readonly CurrencyPair _currencyPair;
        private readonly WebClient _client = new WebClient();

        /// <summary>コンストラクタ</summary>
        /// <param name="currencyPair">通貨ペアを指定します。</param>
        public PublicApi(CurrencyPair currencyPair) => _currencyPair = currencyPair;

        /// <summary>
        /// depth API を実行して結果を得ます。
        /// </summary>
        /// <returns>結果を格納した<see cref="DepthResult"/>オブジェクト</returns>
        public async Task<DepthResult> QueryDepthAsync() => new DepthResult(await DownloadDynamicJsonAsync("depth"));

        /// <summary>
        /// ticker API を実行して結果を得ます。
        /// </summary>
        /// <returns>結果を格納した<see cref="TickerResult"/>オブジェクト</returns>
        public async Task<TickerResult> QueryTickerAsync() => new TickerResult(await DownloadDynamicJsonAsync("ticker"));

        /// <summary>
        /// trade API を実行して結果を得ます。
        /// </summary>
        /// <returns>結果を格納した<see cref="TradesResult"/>の列挙</returns>
        public async Task<IEnumerable<TradesResult>> QueryTradesAsync()
        {
            var url = new Uri($"{Endpoint}/trades/{_currencyPair.Symbol}");
            var jsonString = await _client.DownloadStringTaskAsync(url).ConfigureAwait(false);

            IEnumerable<TradesResult> TradeResultGenerator()
            {
                foreach (var item in JSON.DeserializeDynamic(jsonString)) 
                    yield return new TradesResult(item);
            }

            return TradeResultGenerator();
        }

        /// <summary>
        /// last_price API を実行して結果を得ます
        /// </summary>
        /// <returns>last_price の値</returns>
        public async Task<double> QueryLastPriceAsync()
        {
            var url = new Uri($"{Endpoint}/last_price/{_currencyPair.Symbol}");
            var jsonString = await _client.DownloadStringTaskAsync(url).ConfigureAwait(false);
            return JSON.DeserializeDynamic(jsonString).last_price;
        }

        private async Task<dynamic> DownloadDynamicJsonAsync(string target)
        {
            var url = new Uri($"{Endpoint}/{target}/{_currencyPair.Symbol}");
            var jsonString = await _client.DownloadStringTaskAsync(url).ConfigureAwait(false);
            return JSON.DeserializeDynamic(jsonString);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

    }
}