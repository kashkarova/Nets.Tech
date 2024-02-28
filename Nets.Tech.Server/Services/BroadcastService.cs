using System.Net.WebSockets;
using System.Text;

namespace Nets.Tech.Server.Services
{
    public class BroadcastService : IBroadcastService
    {
        public static event EventHandler<string>? OnNewMessageReceived;

        private IList<WebSocket> WebSockets { get; }

        public BroadcastService()
        {
            WebSockets = [];
            OnNewMessageReceived = null;
        }

        public async Task ConnectAsync(WebSocket webSocket)
        {
            WebSockets.Add(webSocket);
            WebSocketReceiveResult result;

            do
            {
                var buffer = new ArraySegment<byte>(new byte[1024]);
                result = await webSocket.ReceiveAsync(buffer, default);
                OnNewMessageReceived?.Invoke(null, Encoding.UTF8.GetString(buffer).Trim('\0'));
            } while (webSocket.State == WebSocketState.Open && result.MessageType == WebSocketMessageType.Text);

            await webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, default);

            WebSockets.Remove(webSocket);
        }

        public async Task BroadcastAsync(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            var arraySegment = new ArraySegment<byte>(buffer);

            var wsTasks = WebSockets
                .Where(s => s.State == WebSocketState.Open)
                .Select(s => s.SendAsync(arraySegment, WebSocketMessageType.Text, false, default))
                .ToArray();

            await Task.WhenAll(wsTasks);
        }
    }
}