using System.Collections;
using Fleck;
using SpaceCorpsServerShared.Players;
using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerShared;

public class Server()
{
    private WebSocketServer WebSocketServer { get; set; } = null!;
    public RewardServer RewardServer { get; set; } = new();

    public Server(int? port = 8181) : this()
    {
        WebSocketServer = new WebSocketServer($"ws://localhost:{port}");
    }

    public void Start()
    {
        WebSocketServer.Start(ws =>
        {
            ws.OnMessage = Console.WriteLine;
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

    public object? GetStatisticsServer()
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