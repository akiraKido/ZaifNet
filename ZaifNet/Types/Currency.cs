using System;

namespace ZaifNet.Types
{
    public class Currency
    {
        public static readonly Currency Jpy = new Currency("jpy", 1);
        public static readonly Currency Btc = new Currency("btc", 2);
        public static readonly Currency Mona = new Currency("mona", 3);
        public static readonly Currency Xem = new Currency("xem", 4);

        public string Symbol { get; }
        public int Id { get; }
        
        private Currency(string symbol, int id)
        {
            Symbol = symbol;
            Id = id;
        }
    }

}