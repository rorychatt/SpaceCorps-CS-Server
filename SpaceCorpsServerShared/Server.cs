using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using Microsoft.Extensions.Logging;
using SpaceCorpsServerShared;
using SpaceCorpsServerShared.Players;
using SpaceCorpsServerShared.Statistics;

public class Server : IServer
{
    private readonly ConcurrentDictionary<Guid, IPlayer> players = new();
    private readonly ConcurrentDictionary<Guid, WebSocket> sockets = new();
    private IStatisticsServer statisticsServer = new StatisticsServer();
    private readonly ILogger<Server> _logger;
    private int _port { get; }
    public IRewardServer RewardServer { get; private set; } = new RewardServer();
    public Server(ILogger<Server> logger, int port)
    {
        _logger = logger;
        _port = port;
    }

    public Server()
    {
        _logger = new LoggerFactory().CreateLogger<Server>();
    }

    public void Start()
    {
        Start([]);
    }
    public void Start(string[] args)
    {
        HttpListener httpListener = new();
        httpListener.Prefixes.Add($"http://localhost:{_port}/");
        httpListener.Start();
        _logger.LogInformation("Server started at http://localhost:{Port}/", _port);

        _ = ListenForConnectionsAsync(httpListener);


    }

    public async Task ListenForConnectionsAsync(HttpListener httpListener)
    {
        while (true)
        {
            HttpListenerContext httpContext = await httpListener.GetContextAsync();
            if (httpContext.Request.IsWebSocketRequest)
            {
                HttpListenerWebSocketContext webSocketContext = await httpContext.AcceptWebSocketAsync(null);
                _logger.LogInformation("WebSocket connection established");

                WebSocket webSocket = webSocketContext.WebSocket;
                Guid playerId = Guid.NewGuid();
                Player player = new(playerId);
                players[playerId] = player;
                sockets[playerId] = webSocket;

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
        WebSocket webSocket = sockets[player.Id];
        byte[] buffer = new byte[1024 * 4];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (result.MessageType != WebSocketMessageType.Close)
        {
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            _logger.LogInformation("Received from {PlayerId}: {ReceivedMessage}", player.Id, receivedMessage);

            string responseMessage = $"Echo from {player.Id}: {receivedMessage}";
            byte[] responseBuffer = Encoding.UTF8.GetBytes(responseMessage);
            await webSocket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        _logger.LogInformation("WebSocket connection closed for player {PlayerId}", player.Id);

        players.TryRemove(player.Id, out _);
    }
    public async Task DisconnectPlayer(Guid playerId)
    {
        if (players.TryGetValue(playerId, out _))
        {
            var socket = sockets[playerId];
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disconnected by server", CancellationToken.None);
            _logger.LogInformation("Player {PlayerId} has been disconnected", playerId);

            players.TryRemove(playerId, out _);
            sockets.TryRemove(playerId, out _);
        }
    }

    public IEnumerable<IPlayer> GetPlayers()
    {
        return players.Values;
    }

    public void Stop()
    {
        foreach (var socket in sockets.Values)
        {
            if (socket.State == WebSocketState.Open)
            {
                _ = socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server stopped", CancellationToken.None);
            }
        }

        sockets.Clear();
        players.Clear();

        _logger.LogInformation("Server stopped");
    }

    public void SetupStatisticsServer(IStatisticsServer _statisticsServer)
    {
        statisticsServer = _statisticsServer;
    }

    public IStatisticsServer GetStatisticsServer()
    {
        return statisticsServer;
    }

    public IPlayer GetPlayer(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public void SetRewardServer(IRewardServer rewardServer)
    {
        RewardServer = rewardServer;
    }

    public async Task ProcessRewardTickAsync()
    {
        var rewardTasks = new List<Task>();
        foreach (IPlayer player in players.Values)
        {
            async Task HandlePlayerAsync(IPlayer p)
            {
                try
                {
                    var rewardResult = await RewardServer.HandleRewardsForUserAsync(p.Id);
                    if (rewardResult != null)
                    {
                        await statisticsServer.UpdatePlayerFromReward(p, rewardResult);
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

}
