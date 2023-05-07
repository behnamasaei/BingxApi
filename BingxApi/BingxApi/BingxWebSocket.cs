using BingxApi.Domain;
using BingxApi.Domain.Shared;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BingxApi
{
    public class BingxWebSocket
    {
        public async Task ContectToWebSocketAsync(Subscriptions Subscription , string uri , Action<byte[]> callback)
        {
            // Specify the WebSocket server URL
            Uri serverUri = new Uri(uri);

            using (ClientWebSocket clientWebSocket = new ClientWebSocket())
            {
                try
                {
                    // Connect to the WebSocket server
                    await clientWebSocket.ConnectAsync(serverUri, CancellationToken.None);

                    Console.WriteLine("Connected to the WebSocket server.");

                    // Send a message to the server
                    string message = $"{"{"}'id': '{Subscription.Id}', 'reqType': '{Subscription.ReqType}','dataType': '{Subscription.DataType}' {"}"}";

                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    await clientWebSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);

                    // Receive messages from the server
                    while (clientWebSocket.State == WebSocketState.Open)
                    {
                        byte[] receiveBuffer = new byte[1024];
                        WebSocketReceiveResult receiveResult = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);

                        var receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, receiveResult.Count);

                        byte[] receivedBytes = new byte[receiveResult.Count];
                        Array.Copy(receiveBuffer, receivedBytes, receiveResult.Count);
                        callback(receivedBytes);
                    }
                }
                catch (WebSocketException ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    // Close the WebSocket connection
                    if (clientWebSocket.State == WebSocketState.Open)
                        await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing connection.", CancellationToken.None);

                    Console.WriteLine("WebSocket connection closed.");
                }
            }
        }
    }
}
