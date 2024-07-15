using System.Net;
using SpaceCorpsServerShared.Entity;

namespace SpaceCorpsServerShared
{
    public interface IServer
    {
        public Task StartAsync(string[] args);
        public Task ListenForConnectionsAsync(HttpListener httpListener);
        public Task HandleWebSocketConnectionAsync(Player player);
        public void Stop();
        public IEnumerable<Player> GetPlayers();
    }
}