using System.Collections.Concurrent;
using System.Security;
using SpaceCorpsServerShared.Item;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Statistics;
public class RewardServer : IRewardServer
{
    public ConcurrentDictionary<Guid, ConcurrentQueue<IRewardable>> Rewards => new();
    public Task CreateReward(Guid playerId, IRewardable rewardable)
    {
        return Task.Run(() =>
        {
            Rewards.AddOrUpdate(playerId, new ConcurrentQueue<IRewardable>(), (key, value) =>
            {
                value.Enqueue(rewardable);
                return value;
            });
        });
    }

    public Task<ConcurrentQueue<IRewardable>> GetRewardsForUser(Guid playerId)
    {
        return Task.Run(() =>
        {
            if (Rewards.TryGetValue(playerId, out var rewards))
            {
                return rewards;
            }
            throw new ArgumentException("No rewards for user");
        });
    }

    public Task HandleRewardsForUser(Guid playerId)
    {
        return Task.Run(() =>
        {
            if (Rewards.TryGetValue(playerId, out var rewards))
            {
                if (rewards.TryDequeue(out var reward))
                {
                    return reward;
                }
            }
            throw new ArgumentException("No rewards for user");
        });
    }
}
