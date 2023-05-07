using System;
using System.Collections.Generic;
using System.Text;

namespace BingxApi.Domain
{
    public class PartialBookDepth
    {
        public List<string[]>? Bids { get; set; }
        public List<string[]>? Asks { get; set; }
    }
}
