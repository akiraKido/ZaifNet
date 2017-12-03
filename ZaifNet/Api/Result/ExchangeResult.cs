using System;

namespace ZaifNet.Api.Result
{
    public interface IJsonApplyable
    {
        void ApplyJson(dynamic json);
    }
    
    public class ExchangeResult<T> where T : IJsonApplyable, new()
    {
        public bool Success { get; }
        public T Return { get; }
        public string ErrorMessage { get; }

        public ExchangeResult(dynamic json)
        {
            Success = json.success == 1;
            if (!Success)
            {
                Return = default(T);
                ErrorMessage = json.result;
            }
            else
            {
                Return = new T();
                Return.ApplyJson(json["return"]);
            }
        }

        public override string ToString()
        {
            return $"Success     : {Success}{Environment.NewLine}" +
                   $"Return      : [{Environment.NewLine}{Return}]{Environment.NewLine}" +
                   $"ErrorMessage: {ErrorMessage}{Environment.NewLine}";
        }
    }

}