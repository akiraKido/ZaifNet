using System;

namespace ZaifNet.Types
{
    public enum Currency
    {
        Btc,
        Jpy,
        Mona,
        Xem
    }

    public static class CurrencyExtensions
    {
        public static string Symbol(this Currency currency)
        {
            switch (currency)
            {
                case Currency.Btc: return "btc";
                case Currency.Jpy: return "jpy";
                case Currency.Mona: return "mona";
                case Currency.Xem: return "xem";
                default:
                    throw new ArgumentOutOfRangeException(nameof(currency), currency, null);
            }
        }

        public static int Id(this Currency currency)
        {
            switch (currency)
            {
                case Currency.Jpy: return 1;
                case Currency.Btc: return 2;
                case Currency.Mona: return 3;
                case Currency.Xem: return 4;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currency), currency, null);
            }
        }
    }
}