using System;
using System.Collections.Generic;
using System.Text;

namespace BingxApi.Domain
{
    public class WebSocketResponse<T> 
    {
        public string? Id { get; set; }
        public int? Code { get; set; }
        public T Data { get; set; }
        public string? DataType { get; set; }
        public string? Msg { get; set; }
        public long? Timestamp { get; set; }
        public bool? Success { get; set; }
    }
}
