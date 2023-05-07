using System;
using System.Collections.Generic;
using System.Text;

namespace BingxApi.Domain
{
    public class KlineStream
    {
        public long EventTime { get; set; }
        public Kline Kline { get; set; }
        public string EventType { get; set; }
        public string Symbol { get; set; }
    }


    public class Kline
    {
        public string? Close { get; set; }
        public string? High { get; set; }
        public string? Low { get; set; }
        public string? Open { get; set; }
        public string? Interval { get; set; }
        public int? NumberOfTrades { get; set; }
        public string? QuoteAssetVolume { get; set; }
        public string? Symbol { get; set; }
        public long? KlineStartTime { get; set; }
        public long? KlineCloseTime { get; set; }
        public string? BaseAssetVolume { get; set; }
    }
}
