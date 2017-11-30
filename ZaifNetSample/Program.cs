using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ZaifNet.Api;
using ZaifNet.Types;

namespace ZaifNetSample
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var config = Config.Load();
            Console.WriteLine($"{config.Key}, {config.Secret}");
            
            var apiKey = new ApiKey(config.Key, config.Secret);
            using (var api = new ExchangeApi(apiKey))
            {
                var result = await api.ExecuteTradeAsync(CurrencyPair.BtcJpy, TradeType.Bid, 10000, 0.01);
                Console.WriteLine(result);
            }
            
//            var ex = new ExchangeApi(null);
//            using (var publicApi = new PublicApi(CurrencyPair.BtcJpy))
//            {
//                Console.WriteLine(await publicApi.QueryLastPriceAsync());
//                foreach (var tradeResult in await publicApi.QueryTradesAsync())
//                {
//                    Console.WriteLine(tradeResult);
//                }
//                Console.WriteLine(publicApi.QueryTickerAsync().Result);
//                Console.WriteLine(publicApi.QueryDepthAsync().Result);
//            }
        }
    }

    public class Config
    {
        public string Key { get; set; }
        public string Secret { get; set; }
        
        private static Config _instance;
        
        internal static Config Load()
        {
            if (_instance == null)
            {
                _instance = new Config();
                
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName)
                    .AddJsonFile(@"private.json")
                    .Build();
                config.Bind(_instance);
            }
            
            return _instance;
        }
    }
}