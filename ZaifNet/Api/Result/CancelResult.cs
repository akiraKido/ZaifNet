using static System.Environment;

namespace ZaifNet.Api.Result
{
    public class CancelResult : IJsonApplyable
    {
        public int OrderId { get; private set; }
        public Funds Funds { get; private set; }
        
        public void ApplyJson(dynamic json)
        {
            OrderId = json.order_id;
            Funds = new Funds(json.funds);
        }

        public override string ToString()
        {
            return $"OrderId: {OrderId}{NewLine}" +
                   $"Funds  : [{NewLine}{Funds}]{NewLine}";
        }
    }
}