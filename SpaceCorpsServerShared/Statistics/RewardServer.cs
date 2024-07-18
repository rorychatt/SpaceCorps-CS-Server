using System.Collections.Concurrent;
using System.Security;
using SpaceCorpsServerShared.Item;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Statistics;
public class RewardServer : IRewardServer
{
    public ConcurrentDictionary<Guid, ConcurrentQueue<IRewardable>> Rewards => new();
    public void CreateReward(Guid playerId, IRewardable rewardable)
    {
        Rewards.AddOrUpdate(playerId, new ConcurrentQueue<IRewardable>(), (key, value) =>
        {
            value.Enqueue(rewardable);
            return value;
        });
    }

    public ConcurrentQueue<IRewardable> GetRewardsForUser(Guid playerId)
    {
        if (Rewards.TryGetValue(playerId, out var rewards))
        {
            return rewards;
        }
        throw new ArgumentException("No rewards for user");
    }

    public IRewardable HandleRewardsForUser(Guid playerId)
    {
        if (Rewards.TryGetValue(playerId, out var rewards))
        {
            if (rewards.TryDequeue(out var reward))
            {
                return reward;
            }
        }
        throw new ArgumentException("No rewards for user");

    }
}
