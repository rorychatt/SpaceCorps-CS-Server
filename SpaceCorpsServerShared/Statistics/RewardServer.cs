using System.Collections.Concurrent;
using System.Diagnostics;
using System.Security;
using SpaceCorpsServerShared.Item;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Statistics;
public class RewardServer : IRewardServer
{
    public ConcurrentDictionary<Guid, ConcurrentQueue<IRewardable>> Rewards => new();
    public async Task CreateReward(Guid playerId, IRewardable rewardable)
    {
        if (Rewards.TryGetValue(playerId, out var rewards))
        {
            rewards.Enqueue(rewardable);
        }
        else
        {
            var queue = new ConcurrentQueue<IRewardable>();
            queue.Enqueue(rewardable);
            Debug.WriteLine("Before adding: " + Rewards.Count);
            if (Rewards.TryAdd(playerId, queue))
            {
                Debug.WriteLine("Added reward for user");
                Debug.WriteLine("After adding: " + Rewards.Count);
            }
        }

        // Simulate async work if necessary
        await Task.CompletedTask;
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
