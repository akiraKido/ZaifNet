using System;
using static System.Environment;

namespace ZaifNet.Api.Result
{
    public class ExchangeCommonResult
    {
        public bool Success { get; }
        public string ErrorText { get; }
        private readonly ErrorType _errorType;
        public ErrorType ErrorType => _errorType;

        protected ExchangeCommonResult(bool success, string errorText)
        {
            Success = success;
            ErrorText = errorText;
            if (!ErrorType.TryParse(errorText, out _errorType))
            {
                _errorType = ErrorType.Unknown;
            }
        }

        public static ExchangeCommonResult Create(bool success, string errorText)
        {
            return new ExchangeCommonResult(success, errorText);
        }

        public static ExchangeCommonResult Create(dynamic json)
        {
            bool success = json.success == 1;
            string errorText = success ? null : json.error;
            var result = new ExchangeCommonResult(success, errorText);
            return result;
        }
    }

    public class Funds
    {
        public double Jpy { get; }
        public double Btc { get; }
        public double Mona { get; }
        public double Xem { get; }

        public Funds(dynamic json)
        {
            Jpy = json.jpy;
            Btc = json.btc;
            Mona = json.mona;
            Xem = json.ContainsKey("xem") ? json.xem : 0;
        }

        public override string ToString()
        {
            return $"Jpy: {Jpy}{NewLine}" +
                   $"Btc: {Btc}{NewLine}" +
                   $"Mona: {Mona}{NewLine}" +
                   $"Xem: {Xem}{NewLine}";
        }
    }

    public class ErrorType
    {
        public static readonly ErrorType NonceNotIncremented = new ErrorType("nonce not incremented", 1);
        public static readonly ErrorType InvalidAmountParameter = new ErrorType("invalid amount parameter", 2);
        public static readonly ErrorType InsufficientFunds = new ErrorType("insufficient funds", 3);
        public static readonly ErrorType InvalidOrderIdParameter = new ErrorType("invalid order_id parameter", 4);
        public static readonly ErrorType OrderNotFound = new ErrorType("order not found", 5);
        
        public static readonly ErrorType Unknown = new ErrorType("unknown", 254);
        public static readonly ErrorType None = new ErrorType(string.Empty, 255);
        
        public string Symbol { get; }
        public int Id { get; }
        
        private ErrorType(string message, int id)
        {
            Symbol = message;
            Id = id;
        }

        public static bool TryParse(string symbol, out ErrorType errorType)
        {
            errorType = null;
            symbol = symbol.ToLower();
            if (symbol == NonceNotIncremented.Symbol) errorType = NonceNotIncremented;
            if (symbol == InvalidAmountParameter.Symbol) errorType = InvalidAmountParameter;
            if (symbol == InsufficientFunds.Symbol) errorType = InsufficientFunds;
            if (symbol == InvalidOrderIdParameter.Symbol) errorType = InvalidOrderIdParameter;
            if (symbol == OrderNotFound.Symbol) errorType = OrderNotFound;
            if (symbol == Unknown.Symbol) errorType = Unknown;
            if (symbol == None.Symbol) errorType = None;
            return errorType != null;
        }

        public static ErrorType Parse(int id)
        {
            if (id == NonceNotIncremented.Id) return NonceNotIncremented;
            if (id == InvalidAmountParameter.Id) return InvalidAmountParameter;
            if (id == InsufficientFunds.Id) return InsufficientFunds;
            if (id == InvalidOrderIdParameter.Id) return InvalidOrderIdParameter;
            if (id == OrderNotFound.Id) return OrderNotFound;
            if (id == Unknown.Id) return Unknown;
            if (id == None.Id) return None;
            throw new NotImplementedException(id.ToString());
        }
    }
}