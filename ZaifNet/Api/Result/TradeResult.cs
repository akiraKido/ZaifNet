using System;
using ZaifNet.Types;

namespace ZaifNet.Api.Result
{
    public class TradeResult
    {
        public DateTime Date { get; }
        public double Price { get; }
        public double Amount { get; }
        public long TradeId { get; }
        public CurrencyPair CurrencyPair { get; }
        public TradeType TradeType { get; }

        internal TradeResult(dynamic json)
        {
            long date = json.date;
            Date = DateTimeOffset.FromUnixTimeSeconds(date).DateTime.ToLocalTime();
            Price = json.price;
            Amount = json.amount;
            TradeId = json.tid;
            CurrencyPair = CurrencyPair.Parse(
                json.ContainsKey("currency_pair") 
                    ? (string)json.currency_pair 
                    : (string)json.currenty_pair);
            TradeType = TradeType.Parse((string)json.trade_type);
        }

        public override string ToString()
        {
            return $"Date: {Date}\n" +
                   $"Price: {Price}\n" +
                   $"Amount: {Amount}\n" +
                   $"TradeId {TradeId}\n" +
                   $"CurrencyPair: {CurrencyPair.Symbol}\n" +
                   $"TradeType: {TradeType.Symbol}\n";
        }
    }
}