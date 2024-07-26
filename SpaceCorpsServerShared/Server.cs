using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using Microsoft.Extensions.Logging;
using SpaceCorpsServerShared.Players;
using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerShared;
public class Server(ILogger<Server> logger, int port) : IServer
{
    private readonly ConcurrentDictionary<Guid, IPlayer> _players = new();
    private readonly ConcurrentDictionary<Guid, WebSocket> _sockets = new();
    private IStatisticsServer _statisticsServer = new StatisticsServer();
    private readonly ILogger<Server> _logger = logger;
    private int Port { get; } = port;
    public IRewardServer RewardServer { get; private set; } = new RewardServer();
    public Server(int port) : this(new LoggerFactory().CreateLogger<Server>(), port) { }

    public Server() : this(new LoggerFactory().CreateLogger<Server>(), 0)
    {
    }

    public void Start()
    {
        Start([]);
    }
    public void Start(string[] args)
    {
        HttpListener httpListener = new();
        httpListener.Prefixes.Add($"http://localhost:{Port}/");
        httpListener.Start();
        _logger.LogInformation("Server started at http://localhost:{Port}/", Port);

        _ = ListenForConnectionsAsync(httpListener);

    }

    public async Task ListenForConnectionsAsync(HttpListener httpListener)
    {
        while (true)
        {   
            var httpContext = await httpListener.GetContextAsync();
            if (httpContext.Request.IsWebSocketRequest)
            {
                var webSocketContext = await httpContext.AcceptWebSocketAsync(null);
                _logger.LogInformation("WebSocket connection established");
    
                var webSocket = webSocketContext.WebSocket;
                var playerId = Guid.NewGuid();
                Player player = new(playerId);
                _players[playerId] = player;
                _sockets[playerId] = webSocket;
    
                await HandleWebSocketConnectionAsync(player);
            }
            else
            {
                httpContext.Response.StatusCode = 400;
                httpContext.Response.Close();
            }
        }
    }
    public async Task HandleWebSocketConnectionAsync(IPlayer player)
    {
        var webSocket = _sockets[player.Id];
        var buffer = new byte[1024 * 4];
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (result.MessageType != WebSocketMessageType.Close)
        {
            var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            _logger.LogInformation("Received from {PlayerId}: {ReceivedMessage}", player.Id, receivedMessage);

            var responseMessage = $"Echo from {player.Id}: {receivedMessage}";
            var responseBuffer = Encoding.UTF8.GetBytes(responseMessage);
            await webSocket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        _logger.LogInformation("WebSocket connection closed for player {PlayerId}", player.Id);

        _players.TryRemove(player.Id, out _);
    }
    public async Task DisconnectPlayer(Guid playerId)
    {
        if (_players.TryGetValue(playerId, out _))
        {
            var socket = _sockets[playerId];
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disconnected by server", CancellationToken.None);
            _logger.LogInformation("Player {PlayerId} has been disconnected", playerId);

            _players.TryRemove(playerId, out _);
            _sockets.TryRemove(playerId, out _);
        }
    }

    public IEnumerable<IPlayer> GetPlayers()
    {
        return _players.Values;
    }

    public void Stop()
    {
        foreach (var socket in _sockets.Values)
        {
            if (socket.State == WebSocketState.Open)
            {
                _ = socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server stopped", CancellationToken.None);
            }
        }

        _sockets.Clear();
        _players.Clear();

        _logger.LogInformation("Server stopped");
    }

    public void SetupStatisticsServer(IStatisticsServer statisticsServer)
    {
        this._statisticsServer = statisticsServer;
    }

    public IStatisticsServer GetStatisticsServer()
    {
        return _statisticsServer;
    }

    public IPlayer GetPlayerById(Guid playerId)
    {
        return _players[playerId];
    }

    public void SetRewardServer(IRewardServer rewardServer)
    {
        RewardServer = rewardServer;
    }

    public async Task ProcessRewardTickAsync()
    {
        var rewardTasks = new List<Task>();
        foreach (IPlayer player in _players.Values)
        {
            async Task HandlePlayerAsync(IPlayer p)
            {
                try
                {
                    var rewardResult = await RewardServer.HandleRewardsForUserAsync(p.Id);
                    if (rewardResult != null)
                    {
                        await _statisticsServer.UpdatePlayerFromRewardAsync(p, rewardResult);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing rewards for player {p.Id}: {ex.Message}");
                }
            }
            rewardTasks.Add(HandlePlayerAsync(player));
        }
        await Task.WhenAll(rewardTasks);
    }

    public async Task IssueRewardAsync(Guid playerId, IRewardable rewardable)
    {
        await RewardServer.CreateRewardAsync(playerId, rewardable);
    }

}
