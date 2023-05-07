using System;
using System.Collections.Generic;
using System.Text;

namespace BingxApi.Domain
{
    public class TradeStream
    {
        public long EventTime { get; set; }
        public long TradeTime { get; set; }
        public string TradeID { get; set; }
        public string EventType { get; set; }
        public string Price { get; set; }
        public bool BuyerMaker { get; set; }
        public string Quantity { get; set; }
        public string Symbol { get; set; }

    }
}
