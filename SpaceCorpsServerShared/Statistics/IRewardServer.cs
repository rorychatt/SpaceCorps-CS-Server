using System.Collections.Concurrent;

namespace SpaceCorpsServerShared.Statistics;

public interface IRewardServer
{
    ConcurrentDictionary<Guid, ConcurrentQueue<IRewardable>> Rewards{get;}
    Task CreateReward(Guid playerId, IRewardable rewardable);
    Task HandleRewardsForUser(Guid playerId);
    Task<ConcurrentQueue<IRewardable>> GetRewardsForUser(Guid playerId);
}