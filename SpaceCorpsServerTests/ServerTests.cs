using System.Net;
using System.Net.WebSockets;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SpaceCorpsServerShared;
using SpaceCorpsServerShared.Players;
using SpaceCorpsServerShared.Statistics;

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

    [Fact]
    public async void TestServer_RegistersReward()
    {
        var port = 2003;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);

        server.Start([]);

        var client = new ClientWebSocket();
        await client.ConnectAsync(new Uri($"ws://localhost:{port}"), CancellationToken.None);

        var player = server.GetPlayers().First();

        await server.IssueRewardAsync(player.Id, new ItemReward(player.Id, 1));

        server.RewardServer.GetRewardsForUser(player.Id).Count.Should().Be(1);
    }

    [Fact]
    public async void TestServer_IssuesAndExecutes_StatsReward()
    {
        var port = 2005;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);

        server.Start([]);

        var client = new ClientWebSocket();
        await client.ConnectAsync(new Uri($"ws://localhost:{port}"), CancellationToken.None);

        var player = server.GetPlayers().First();

        var stats = new Stats();
        stats.SetThulium(10000);

        await server.IssueRewardAsync(player.Id, new StatsReward(player.Id, stats));

        await server.ProcessRewardTickAsync();

        server.GetPlayer(player.Id).GetStats().GetThulium().Should().Be(10001);
    }


}