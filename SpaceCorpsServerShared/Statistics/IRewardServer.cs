using System.Collections.Concurrent;

namespace SpaceCorpsServerShared.Statistics;

public interface IRewardServer
{
    public ConcurrentDictionary<Guid, ConcurrentQueue<IRewardable>> GetRewards();
    void CreateReward(Guid playerId, IRewardable rewardable);
    IRewardable? HandleRewardsForUser(Guid playerId);
    ConcurrentQueue<IRewardable> GetRewardsForUser(Guid playerId);
}