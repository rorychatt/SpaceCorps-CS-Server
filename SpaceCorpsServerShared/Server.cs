using System.Collections;
using Fleck;
using SpaceCorpsServerShared.Players;
using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerShared;

public class Server()
{
    private WebSocketServer WebSocketServer { get; set; } = null!;
    public RewardServer RewardServer { get; set; } = new();
    public StatisticsServer StatisticsServer { get; set; } = new();
    public List<IWebSocketConnection> WebSocketConnections { get; set; } = [];

    public Server(int? port = 8181) : this()
    {
        WebSocketServer = new WebSocketServer($"ws://localhost:{port}");
    }

    public void Start()
    {
        WebSocketServer.Start(ws =>
        {
            ws.OnOpen = () => { WebSocketConnections.Add(ws); };
            ws.OnMessage = Console.WriteLine;
            ws.OnClose = () => { WebSocketConnections.Remove(ws); };
        });
    }

    public void Stop()
    {
        WebSocketServer.Dispose();
    }

    public IEnumerable<Player> GetPlayers()
    {
        throw new NotImplementedException();
    }

    public async Task IssueRewardAsync(Guid playerId, IRewardable reward)
    {
        throw new NotImplementedException();
    }

    public async Task ProcessTick()
    {
        throw new NotImplementedException();
    }

    public Player? GetPlayerById(Guid playerId)
    {
        throw new NotImplementedException();
    }
}