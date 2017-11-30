using System;
using System.Threading.Tasks;
using ZaifNet.Api;

namespace ZaifNetSample
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            using (var publicApi = new PublicApi(CurrencyPair.BtcJpy))
            {
                Console.WriteLine(await publicApi.QueryLastPriceAsync());
                foreach (var tradeResult in await publicApi.QueryTradesAsync())
                {
                    Console.WriteLine(tradeResult);
                }
                Console.WriteLine(publicApi.QueryTickerAsync().Result);
                Console.WriteLine(publicApi.QueryDepthAsync().Result);
            }
        }
    }
}