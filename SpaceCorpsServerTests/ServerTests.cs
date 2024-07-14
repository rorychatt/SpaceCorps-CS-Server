using System.Net;
using System.Net.WebSockets;
using Microsoft.Extensions.Logging;
using Moq;
using SpaceCorpsServerShared;

namespace SpaceCorpsServerTests;

public class ServerTests
{

    [Fact]
    public void TestServer_StartAsync()
    {
        var port = 1000;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);
        _ = server.StartAsync([]);
        loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()
            ),
            Times.Once
        );

        Server.Stop();
    }

    [Fact]
    public void TestServer_Connects()
    {
        var port = 1001;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);

        _ = server.StartAsync([]);
        var client = new ClientWebSocket();
        client.ConnectAsync(new Uri($"ws://localhost:{port}"), CancellationToken.None).Wait();
        Assert.Equal(WebSocketState.Open, client.State);

        Server.Stop();
    }

    [Fact]
    public void TestServer_CreatesPlayer_OnJoin()
    {
        var port = 1002;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);

        _ = server.StartAsync([]);
        var client = new ClientWebSocket();
        client.ConnectAsync(new Uri($"ws://localhost:{port}"), CancellationToken.None).Wait();
        Assert.Equal(WebSocketState.Open, client.State);
        server.GetPlayers().Count().CompareTo(0);

        Server.Stop();
    }

}