using System;
using ZaifNet.Types;

namespace ZaifNet.Api
{
    public class CurrencyPair
    {
        public static readonly CurrencyPair BtcJpy = new CurrencyPair(Currency.Btc, Currency.Jpy);
        public static readonly CurrencyPair MonaJpy = new CurrencyPair(Currency.Mona, Currency.Jpy);
        public static readonly CurrencyPair XemJpy = new CurrencyPair(Currency.Xem, Currency.Jpy);
        public static readonly CurrencyPair XemBtc = new CurrencyPair(Currency.Xem, Currency.Btc);
        public static readonly CurrencyPair MonaBtc = new CurrencyPair(Currency.Mona, Currency.Btc);

        public string Symbol { get; }
        public int Id { get; }
        
        private CurrencyPair(Currency left, Currency right)
        {
            Symbol = $"{left.Symbol()}_{right.Symbol()}";
            Id = (left.Id() << 8) | right.Id();
        }

        public static CurrencyPair Parse(string s)
        {
            if (s == BtcJpy.Symbol) return BtcJpy;
            if (s == MonaJpy.Symbol) return MonaJpy;
            if (s == XemJpy.Symbol) return XemJpy;
            if (s == XemBtc.Symbol) return XemBtc;
            if (s == MonaBtc.Symbol) return MonaBtc;
            throw new NotImplementedException(s);
        }
    }
}