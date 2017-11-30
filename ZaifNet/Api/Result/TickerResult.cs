using Jil;

namespace ZaifNet.Api.Result
{
    public class TickerResult
    {
        public double Last { get; }
        public double High { get; }
        public double Low { get; }
        public double Vwap { get; }
        public double Volume { get; }
        public double Bid { get; }
        public double Ask { get; }

        internal TickerResult(dynamic json)
        {
            Last = json.last;
            High = json.high;
            Low = json.low;
            Vwap = json.vwap;
            Volume = json.volume;
            Bid = json.bid;
            Ask = json.ask;
        }

        public override string ToString()
        {
            return $"Last: {Last}\n" +
                   $"High: {High}\n" +
                   $"Low: {Low}\n" +
                   $"Vwap: {Vwap}\n" +
                   $"Volume: {Volume}\n" +
                   $"Bid: {Bid}\n" +
                   $"Ask: {Ask}\n";
        }
    }
}