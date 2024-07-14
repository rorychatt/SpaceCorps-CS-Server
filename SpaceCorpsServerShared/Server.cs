using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Logging;
using SpaceCorpsServerShared.Entity;

public class Server
{
    private static ConcurrentDictionary<Guid, Player> players = new ConcurrentDictionary<Guid, Player>();
    private static ConcurrentDictionary<Guid, WebSocket> sockets = new ConcurrentDictionary<Guid, WebSocket>();
    private readonly ILogger<Server> _logger;
    public int _port { get; }

    public Server(ILogger<Server> logger, int port)
    {
        _logger = logger;
        _port = port;
    }

    public Server()
    {
        _logger = new LoggerFactory().CreateLogger<Server>();
    }
    public async Task StartAsync(string[] args)
    {
        HttpListener httpListener = new HttpListener();
        httpListener.Prefixes.Add($"http://localhost:{_port}/");
        httpListener.Start();
        _logger.LogInformation($"Server started at http://localhost:{_port}/");

        Task listenTask = ListenForConnectionsAsync(httpListener);

        await Task.WhenAll(listenTask);
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
                Player player = new Player(playerId);
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
    public async Task HandleWebSocketConnectionAsync(Player player)
    {
        WebSocket webSocket = sockets[player.Id];
        byte[] buffer = new byte[1024 * 4];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (result.MessageType != WebSocketMessageType.Close)
        {
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            _logger.LogInformation($"Received from {player.Id}: {receivedMessage}");

            string responseMessage = $"Echo from {player.Id}: {receivedMessage}";
            byte[] responseBuffer = Encoding.UTF8.GetBytes(responseMessage);
            await webSocket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        _logger.LogInformation($"WebSocket connection closed for player {player.Id}");

        players.TryRemove(player.Id, out _);
    }
    public async Task DisconnectPlayer(Guid playerId)
    {
        if (players.TryGetValue(playerId, out _))
        {
            var socket = sockets[playerId];
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disconnected by server", CancellationToken.None);
            _logger.LogInformation($"Player {playerId} has been disconnected");

            players.TryRemove(playerId, out _);
            sockets.TryRemove(playerId, out _);
        }
    }

    public IEnumerable<Player> GetPlayers()
    {
        return players.Values;
    }

    public static void Stop()
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
    }
}
