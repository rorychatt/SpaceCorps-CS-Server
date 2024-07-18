using System.Collections.Concurrent;

namespace SpaceCorpsServerShared.Statistics;

public interface IRewardServer
{
    ConcurrentDictionary<Guid, ConcurrentQueue<IReward>> Rewards { get; }
    Task CreateReward(Guid playerId, IRewardable rewardable);
    Task<IReward> HandleRewardsForUser(Guid playerId);
}