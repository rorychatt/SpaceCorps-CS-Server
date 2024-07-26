using System.Net.WebSockets;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SpaceCorpsServerShared;
using SpaceCorpsServerShared.Players;
using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerTests;

public class ServerTests
{

    [Fact]
    public void TestServer_Start_No_Crash()
    {
        const int port = 2000;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);
        server.Start();
        Assert.True(true);
    }

    [Fact]
    public async void TestServer_Connects()
    {
        const int port = 2001;
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
        const int port = 2002;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);

        server.Start([]);

        var client = new ClientWebSocket();
        await client.ConnectAsync(new Uri($"ws://localhost:{port}"), CancellationToken.None);

        Assert.Equal(WebSocketState.Open, client.State);
        
        var players = server.GetPlayers();
        Assert.Single(players);

        server.Stop();
    }

    [Fact]
    public async void TestServer_RegistersReward()
    {
        const int port = 2003;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);

        server.Start([]);

        var client = new ClientWebSocket();
        await client.ConnectAsync(new Uri($"ws://localhost:{port}"), CancellationToken.None);

        var player = server.GetPlayers().First();

        await server.IssueRewardAsync(player.Id, new ItemReward(player.Id, 1));


        var receivedReward = server.RewardServer.GetRewardsForUser(player.Id).First();
        receivedReward.Should().BeOfType<ItemReward>();

        server.Stop();

    }

    [Fact]
    public async void TestServer_IssuesAndExecutes_StatsReward()
    {
        const int port = 2005;
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

        server.GetPlayerById(player.Id).GetStats().GetThulium().Should().Be(10000);

        server.Stop();
    }


}