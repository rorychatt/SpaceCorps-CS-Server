using System.Net.WebSockets;
using Microsoft.Extensions.Logging;
using Moq;
using SpaceCorpsServerShared;

namespace SpaceCorpsServerTests;

public class ServerTests
{
    private readonly Mock<ILogger<Server>> loggerMock;
    private readonly Server server;

    public ServerTests()
    {
        loggerMock = new Mock<ILogger<Server>>();
        server = new Server(loggerMock.Object, 5000);
    }

    [Fact]
    public void TestServer_Start_Logs_Message()
    {
        var cts = new CancellationTokenSource();
        cts.CancelAfter(1000);

        var startTask = server.StartAsync(cts.Token);

        loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == "Server started."),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
            Times.Once);
    }

    [Fact]
    public async Task TestServer_Logs_ClientConnected_Message()
    {
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, 5000);

        var webSocketMock = new Mock<WebSocket>();
        webSocketMock.Setup(x => x.State).Returns(WebSocketState.Open);

        var clientId = Guid.NewGuid().ToString();

        await server.HandleClientAsync(clientId, webSocketMock.Object, CancellationToken.None);

        loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == $"Client connected: {clientId}"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
            Times.Once);
    }

    [Fact]
    public async Task TestServer_Adds_Player_On_Client_Connect()
    {
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, 5000);

        var webSocketMock = new Mock<WebSocket>();
        webSocketMock.Setup(x => x.State).Returns(WebSocketState.Open);

        var clientId = Guid.NewGuid().ToString();

        await server.HandleClientAsync(clientId, webSocketMock.Object, CancellationToken.None);

        Assert.True(server.GetPlayers().ContainsKey(clientId));
    }

    [Fact]
    public async Task TestServer_Removes_Player_On_Client_Disconnect()
    {
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, 5000);

        var webSocketMock = new Mock<WebSocket>();
        webSocketMock.Setup(x => x.State).Returns(WebSocketState.Open);

        var clientId = Guid.NewGuid().ToString();

        await server.HandleClientAsync(clientId, webSocketMock.Object, CancellationToken.None);

        webSocketMock.Setup(x => x.State).Returns(WebSocketState.Closed);

        await server.HandleClientAsync(clientId, webSocketMock.Object, CancellationToken.None);

        Assert.False(server.GetPlayers().ContainsKey(clientId));
    }



}