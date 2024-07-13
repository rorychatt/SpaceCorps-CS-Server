using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace SpaceCorpsServerShared;

public class Server
{
    private readonly ILogger<Server> _logger;
    private readonly HttpListener _httpListener;
    private readonly ConcurrentDictionary<string, WebSocket> _clientConnections;

    public Server() : this(NullLogger<Server>.Instance, 5000)
    {
    }

    public Server(ILogger<Server> logger, int port)
    {
        _logger = logger;
        _httpListener = new HttpListener();
        _httpListener.Prefixes.Add($"http://*:{port}/");
        _clientConnections = new ConcurrentDictionary<string, WebSocket>();
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        _httpListener.Start();
        _logger.LogInformation("Server started.");
        Console.WriteLine("Server started.");

        while (!cancellationToken.IsCancellationRequested)
        {
            var context = await _httpListener.GetContextAsync();
            if (context.Request.IsWebSocketRequest)
            {
                var webSocketContext = await context.AcceptWebSocketAsync(null);
                var webSocket = webSocketContext.WebSocket;
                var clientId = Guid.NewGuid().ToString();

                if (_clientConnections.TryAdd(clientId, webSocket))
                {
                    _logger.LogInformation($"Client connected: {clientId}");
                }

                _ = HandleClientAsync(clientId, webSocket, cancellationToken);
            }
        }
    }

    private async Task HandleClientAsync(string clientId, WebSocket webSocket, CancellationToken cancellationToken)
    {
        try
        {
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
            if (_clientConnections.TryRemove(clientId, out _))
            {
                _logger.LogInformation($"Client removed: {clientId}");
            }
        }
    }
}
