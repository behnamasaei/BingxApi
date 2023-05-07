using BingxApi.Domain;
using BingxApi.Domain.Shared;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BingxApi.StandardContract
{
    public class WebSocketMarketData
    {
        public async Task TradeStreamsAsync(Subscriptions Subscription, Action<WebSocketResponse<TradeStream>> callback)
        {

            BingxWebSocket webSocket = new BingxWebSocket();
            await webSocket.ContectToWebSocketAsync(Subscription, BingxConsts.ContractSocketBaseAddress, data =>
            {
                JObject json = JObject.Parse(BingxTools.ReadGzip(data));

                try
                {
                    // Assuming the data is received from some source
                    WebSocketResponse<TradeStream> response = new WebSocketResponse<TradeStream>
                    {
                        Code = Convert.ToInt32(json["code"]),
                        DataType = json["dataType"].ToString(),
                        Success = (bool?)json["success"],
                        Data = new TradeStream
                        {
                            EventTime = (long)json["data"]["E"],
                            TradeTime = (long)json["data"]["T"],
                            TradeID = (string)json["data"]["t"],
                            EventType = (string)json["data"]["e"],
                            Price = (string)json["data"]["p"],
                            BuyerMaker = (bool)json["data"]["m"],
                            Quantity = (string)json["data"]["q"],
                            Symbol = (string)json["data"]["s"]

                        }
                    };
                    // Invoke the callback with the received data
                    callback(response);
                }
                catch (Exception ex)
                {
                    // Console.WriteLine(ex);
                }

            });
        }

        public async Task KlineStreamsAsync(Subscriptions Subscription, Action<WebSocketResponse<KlineStream>> callback)
        {
            BingxWebSocket webSocket = new BingxWebSocket();
            await webSocket.ContectToWebSocketAsync(Subscription, BingxConsts.ContractSocketBaseAddress, data =>
            {
                JObject json = JObject.Parse(BingxTools.ReadGzip(data));

                try
                {
                    // Assuming the data is received from some source
                    WebSocketResponse<KlineStream> response = new WebSocketResponse<KlineStream>
                    {
                        Code = Convert.ToInt32(json["code"]),
                        DataType = json["dataType"].ToString(),
                        Success = (bool?)json["success"],
                        Data = new KlineStream
                        {
                            EventTime = (long)json["data"]["E"],
                            EventType = (string)json["data"]["e"],
                            Symbol = (string)json["data"]["s"],

                            Kline = new Kline
                            {
                                Close = (string)json["data"]["K"]["c"],
                                High = (string)json["data"]["K"]["h"],
                                Low = (string)json["data"]["K"]["l"],
                                Open = (string)json["data"]["K"]["o"],
                                Interval = (string)json["data"]["K"]["i"],
                                NumberOfTrades = (int?)json["data"]["K"]["n"],
                                QuoteAssetVolume = (string)json["data"]["K"]["q"],
                                Symbol = (string)json["data"]["K"]["s"],
                                KlineCloseTime = (long?)json["data"]["K"]["T"],
                                KlineStartTime = (long?)json["data"]["K"]["t"],
                                BaseAssetVolume = (string)json["data"]["K"]["v"]
                            }
                        }
                    };
                    // Invoke the callback with the received data
                    callback(response);
                }
                catch (Exception ex)
                {
                    // Console.WriteLine(ex);
                }
            });
        }

        public async Task PartialBookDepthStreamsAsync(Subscriptions Subscription, Action<WebSocketResponse<PartialBookDepth>> callback)
        {
            BingxWebSocket webSocket = new BingxWebSocket();
            await webSocket.ContectToWebSocketAsync(Subscription, BingxConsts.ContractSocketBaseAddress, data =>
            {
                JObject json = JObject.Parse(BingxTools.ReadGzip(data));

                try
                {
                    // Assuming the data is received from some source
                    WebSocketResponse<PartialBookDepth> response = new WebSocketResponse<PartialBookDepth>
                    {
                        Code = Convert.ToInt32(json["code"]),
                        DataType = json["dataType"].ToString(),
                        Success = (bool?)json["success"],
                        Data = new PartialBookDepth 
                        {
                            Asks = BingxTools.ConvertJTokenToList(json["data"]["asks"]),
                            Bids = BingxTools.ConvertJTokenToList(json["data"]["bids"])
                        }
                    };

                    // Invoke the callback with the received data
                    callback(response);
                }
                catch (Exception ex)
                {
                    // Console.WriteLine(ex);
                }
            });
        }
    }
}
