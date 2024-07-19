using System.Collections.Concurrent;

namespace SpaceCorpsServerShared.Statistics;

public interface IRewardServer
{
    Dictionary<Guid, Queue<IRewardable>> Rewards{get;}
    void CreateReward(Guid playerId, IRewardable rewardable);
    IRewardable HandleRewardsForUser(Guid playerId);
    Queue<IRewardable> GetRewardsForUser(Guid playerId);
}