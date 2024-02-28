using System.Net.WebSockets;

namespace Nets.Tech.Server.Services
{
    public interface IBroadcastService
    {
        Task ConnectAsync(WebSocket webSocket);

        Task BroadcastAsync(string message);
    }
}
