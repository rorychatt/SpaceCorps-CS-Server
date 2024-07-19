using System.Net;
using System.Net.WebSockets;
using Microsoft.Extensions.Logging;
using Moq;
using SpaceCorpsServerShared;

namespace SpaceCorpsServerTests.ServerTests;

public class ServerTests
{

    [Fact]
    public void TestServer_Start_No_Crash()
    {
        var port = 2000;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);
        server.Start();
        Assert.True(true);
    }

    [Fact]
    public async void TestServer_Connects()
    {
        var port = 2001;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);

        server.Start([]);
        var client = new ClientWebSocket();
        await client.ConnectAsync(new Uri($"ws://localhost:{port}"), CancellationToken.None);
        Assert.Equal(WebSocketState.Open, client.State);

        server.Stop();
    }

    [Fact]
    public async void TestServer_CreatesPlayer_OnJoin()
    {
        var port = 2002;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);

        server.Start([]);

        var client = new ClientWebSocket();
        await client.ConnectAsync(new Uri($"ws://localhost:{port}"), CancellationToken.None);

        Assert.Equal(WebSocketState.Open, client.State);
        server.GetPlayers().Count().Equals(1);

        server.Stop();
    }

}