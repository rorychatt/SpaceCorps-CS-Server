using SpaceCorpsServerShared.Players;
using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerTests.RewardServerTests;

public class RewardServerTests
{
    [Fact]
    public void TestStatsReward()
    {
        var playerId = Guid.NewGuid();
        var stats = new Stats();
        var reward = new StatsReward(playerId, stats);
        Assert.Equal(playerId, reward.PlayerId);
        Assert.Equal(stats, reward.Statistics);
    }

    [Fact]
    public void TestItemReward()
    {
        var playerId = Guid.NewGuid();
        var itemId = 1;
        var reward = new ItemReward(playerId, itemId);
        Assert.Equal(playerId, reward.PlayerId);
        Assert.Equal(itemId, reward.ItemId);
    }

    [Fact]
    public async void Adds_ItemReward_To_RewardServer()
    {
        var playerId = Guid.NewGuid();
        var itemId = 1;
        var reward = new ItemReward(playerId, itemId);
        var rewardsServer = new RewardServer();
        await rewardsServer.CreateRewardAsync(playerId, reward);

        var rewardsFromServer = rewardsServer.GetRewardsForUser(playerId);
        Assert.Single(rewardsFromServer);
    }

    [Fact]
    public async void Add_StatsReward_To_RewardServer()
    {
        var playerId = Guid.NewGuid();
        var stats = new Stats();
        var reward = new StatsReward(playerId, stats);
        var rewardsServer = new RewardServer();
        await rewardsServer.CreateRewardAsync(playerId, reward);

        var rewardsFromServer = rewardsServer.GetRewardsForUser(playerId);
        Assert.Single(rewardsFromServer);
    }

    [Fact]
    public async void Add_ThuliumReward_To_RewardServer_With_ValueCheck()
    {
        var playerId = Guid.NewGuid();
        var stats = new Stats();
        stats.SetThulium(10000);
        var reward = new StatsReward(playerId, stats);
        var rewardsServer = new RewardServer();
        await rewardsServer.CreateRewardAsync(playerId, reward);

        var rewardsFromServer = rewardsServer.GetRewardsForUser(playerId);
        rewardsFromServer.TryDequeue(out var rewardable);
        var statsReward = rewardable as StatsReward;

        Assert.Equal(10000, statsReward!.Statistics.GetThulium());
    }

}