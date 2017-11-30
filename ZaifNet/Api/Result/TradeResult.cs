using System;
using System.Runtime.CompilerServices;

namespace ZaifNet.Api.Result
{
    public class TradeResult : ExchangeCommonResult
    {
        public double Received { get; private set; }
        public double Remains { get; set; }
        public long OrderId { get; set; }
        public Funds Funds { get; set; }

        protected TradeResult(bool success, string errorText) : base(success, errorText)
        {
        }

        public static TradeResult Create(dynamic json)
        {
            ExchangeCommonResult parent = ExchangeCommonResult.Create(json);
            var result = new TradeResult(parent.Success, parent.ErrorText);
            if (!result.Success)
            {
                result.Received = Double.NaN;
                result.Remains = Double.NaN;
                result.OrderId = 0;
                result.Funds = null;
                return result;
            }

            var returnJson = json["return"];
            result.Received = returnJson.received;
            result.Remains = returnJson.remains;
            result.OrderId = returnJson.order_id;
            result.Funds = new Funds(returnJson.funds);
            return result;
        }
    }
}