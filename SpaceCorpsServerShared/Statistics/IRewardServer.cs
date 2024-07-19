using System.Collections.Concurrent;

namespace SpaceCorpsServerShared.Statistics;

public interface IRewardServer
{
    ConcurrentDictionary<Guid, ConcurrentQueue<IRewardable>> Rewards{get;}
    void CreateReward(Guid playerId, IRewardable rewardable);
    IRewardable HandleRewardsForUser(Guid playerId);
    ConcurrentQueue<IRewardable> GetRewardsForUser(Guid playerId);
}