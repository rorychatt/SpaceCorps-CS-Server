using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SpaceCorpsServerShared.Entity;

namespace SpaceCorpsServerShared;

public class Server
{
    private readonly ILogger<Server> _logger;
    private readonly HttpListener _httpListener;
    private readonly ConcurrentDictionary<string, WebSocket> _clientConnections;
    private readonly ConcurrentDictionary<string, Player> _players;

    public Server() : this(NullLogger<Server>.Instance, 5000)
    {
    }

    public Server(ILogger<Server> logger, int port)
    {
        _logger = logger;
        _httpListener = new HttpListener();
        _httpListener.Prefixes.Add($"http://*:{port}/");
        _clientConnections = new ConcurrentDictionary<string, WebSocket>();
        _players = new ConcurrentDictionary<string, Player>();
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        _httpListener.Start();
        _logger.LogInformation("Server started.");

        while (!cancellationToken.IsCancellationRequested)
        {
            var context = await _httpListener.GetContextAsync();
            if (context.Request.IsWebSocketRequest)
            {
                var webSocketContext = await context.AcceptWebSocketAsync(null);
                var webSocket = webSocketContext.WebSocket;
                var clientId = Guid.NewGuid().ToString();
                _ = HandleClientAsync(clientId, webSocket, cancellationToken);
            }
        }
    }

    public async Task HandleClientAsync(string clientId, WebSocket webSocket, CancellationToken cancellationToken)
    {
        try
        {
            _AddClientConnection(clientId, webSocket);
            _AddPlayer(clientId, new Player());

            var player = new Player();
            var buffer = new byte[1024];
            while (webSocket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client initiated close", cancellationToken);
                    _logger.LogInformation($"Client disconnected: {clientId}");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error handling client: {clientId}");
        }
        finally
        {
            _DisconnectClient(clientId);
        }
    }

    public void Stop()
    {
        _httpListener.Stop();
        _httpListener.Close();
        _logger.LogInformation("Server stopped.");
    }

    public ConcurrentDictionary<string, Player> GetPlayers()
    {
        return _players;
    }

    public ConcurrentDictionary<string, WebSocket> GetClientConnections()
    {
        return _clientConnections;
    }

    private void _AddPlayer(string clientId, Player player)
    {
        if (_players.TryAdd(clientId, player))
        {
            _logger.LogInformation($"Player added: {clientId}");
        }
    }

    private void _RemovePlayer(string clientId)
    {
        if (_players.TryRemove(clientId, out _))
        {
            _logger.LogInformation($"Player removed: {clientId}");
        }
    }

    private void _AddClientConnection(string clientId, WebSocket webSocket)
    {
        if (_clientConnections.TryAdd(clientId, webSocket))
        {
            _logger.LogInformation($"Client connected: {clientId}");
        }
    }

    private void _DisconnectClient(string clientId)
    {
        if (_clientConnections.TryRemove(clientId, out _))
        {
            _logger.LogInformation($"Client removed: {clientId}");
        }

        _RemovePlayer(clientId);
    }

}
