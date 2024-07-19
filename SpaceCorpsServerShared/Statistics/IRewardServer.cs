using System.Collections.Concurrent;

namespace SpaceCorpsServerShared.Statistics;

public interface IRewardServer
{
    public ConcurrentDictionary<Guid, ConcurrentQueue<IRewardable>> GetRewards();
    Task CreateRewardAsync(Guid playerId, IRewardable rewardable);
    Task<IRewardable?> HandleRewardsForUserAsync(Guid playerId);
    ConcurrentQueue<IRewardable> GetRewardsForUser(Guid playerId);
}