using System.Net;
using SpaceCorpsServerShared.Entity;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared
{
    public interface IServer
    {
        public void Start(string[] args);
        public Task ListenForConnectionsAsync(HttpListener httpListener);
        public Task HandleWebSocketConnectionAsync(Player player);
        public void Stop();
        public IEnumerable<Player> GetPlayers();

    }
}