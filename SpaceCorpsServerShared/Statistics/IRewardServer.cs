using System.Collections.Concurrent;

namespace SpaceCorpsServerShared.Statistics;

public interface IRewardServer
{
    public ConcurrentDictionary<Guid, ConcurrentQueue<IRewardable>> GetRewards();
    Task CreateReward(Guid playerId, IRewardable rewardable);
    Task<IRewardable?> HandleRewardsForUser(Guid playerId);
    ConcurrentQueue<IRewardable> GetRewardsForUser(Guid playerId);
}