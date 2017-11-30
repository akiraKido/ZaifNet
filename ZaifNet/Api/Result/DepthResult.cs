using System.Collections.Generic;

namespace ZaifNet.Api.Result
{
    public class DepthResult
    {
        public class Item
        {
            public double Price { get; }
            public double Volume { get; }

            internal Item(dynamic json)
            {
                Price = json[0];
                Volume = json[1];
            }

            public override string ToString() => $"[price:{Price}, volume:{Volume}]";
        }
        
        public IReadOnlyList<Item> Asks => _asks;
        private readonly List<Item> _asks = new List<Item>();

        public IReadOnlyList<Item> Bids => _bids;
        private readonly List<Item> _bids = new List<Item>();

        internal DepthResult(dynamic json)
        {
            foreach (var ask in json.asks)
            {
                _asks.Add(new Item(ask));
            }
            foreach (var bid in json.bids)
            {
                _bids.Add(new Item(bid));
            }
        }

        public override string ToString()
        {
            return $"asks: {{{string.Join(",", _asks)}}},\nbids: {{{string.Join(",", _bids)}}}";
        }
    }
}