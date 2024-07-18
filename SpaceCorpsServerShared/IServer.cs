using System.Net;
using SpaceCorpsServerShared.Players;
using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerShared
{
    public interface IServer
    {
        public void Start();
        public void Start(string[] args);
        public Task ListenForConnectionsAsync(HttpListener httpListener);
        public Task HandleWebSocketConnectionAsync(IPlayer player);
        public void Stop();
        public IEnumerable<IPlayer> GetPlayers();
        public IPlayer GetPlayer(Guid playerId);
        public void SetupStatisticsServer(IStatisticsServer statisticsServer);

    }
}