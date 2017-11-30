using System;

namespace ZaifNet.Types
{
    public class TradeType
    {
        public static readonly TradeType Bid = new TradeType("bid", 1);
        public static readonly TradeType Ask = new TradeType("ask", 2);
        
        public string Symbol { get; }
        public int Id { get; }
        
        private TradeType(string symbol, int id)
        {
            Symbol = symbol;
            Id = id;
        }

        public static TradeType Parse(string s)
        {
            if (s == Bid.Symbol) return Bid;
            if (s == Ask.Symbol) return Ask;
            throw new NotImplementedException(s);
        }
    }
}