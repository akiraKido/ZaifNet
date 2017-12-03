using static System.Environment;

namespace ZaifNet.Api.Result
{
    public class TradeResult : IJsonApplyable
    {
        public double Received { get; private set; }
        public double Remains { get; set; }
        public long OrderId { get; set; }
        public Funds Funds { get; set; }

        public void ApplyJson(dynamic json)
        {
            Received = json.received;
            Remains = json.remains;
            OrderId = json.order_id;
            Funds = new Funds(json.funds);
        }

        public override string ToString()
        {
            return $"Received: {Received}{NewLine}" +
                   $"Remains : {Remains}{NewLine}" +
                   $"OrderId : {OrderId}{NewLine}" +
                   $"Funds   : [{NewLine}{Funds}]{NewLine}";
        }
    }
}