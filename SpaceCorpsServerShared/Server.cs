using System.Collections;
using Fleck;
using SpaceCorpsServerShared.Players;
using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerShared;

public class Server()
{
    private WebSocketServer WebSocketServer { get; } = null!;
    public RewardServer RewardServer { get; } = new();
    public StatisticsServer StatisticsServer { get; } = new();
    private readonly Dictionary<int, IWebSocketConnection> _webSocketConnections = [];
    private readonly Dictionary<string, Player> _players = [];

    public Server(int? port = 8181) : this()
    {
        WebSocketServer = new WebSocketServer($"ws://localhost:{port}");
    }

    public void Start()
    {
        WebSocketServer.Start(ws =>
        {
            ws.OnOpen = () => { _webSocketConnections.TryAdd(ws.GetHashCode(), ws); };
            ws.OnMessage = Console.WriteLine;
            ws.OnClose = () => { _webSocketConnections.Remove(ws.GetHashCode()); };
        });
    }

    public void Stop()
    {
        WebSocketServer.Dispose();
    }

    public IEnumerable<Player> GetPlayers()
    {
        return _players.Values;
    }

    public async Task IssueRewardAsync(Guid playerId, IRewardable reward)
    {
        await RewardServer.CreateRewardAsync(playerId, reward);
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