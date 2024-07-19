using System.Collections.Concurrent;
using System.Diagnostics;
using System.Security;
using SpaceCorpsServerShared.Item;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Statistics;
public class RewardServer : IRewardServer
{
    public ConcurrentDictionary<Guid, ConcurrentQueue<IRewardable>> Rewards => new();
    public void CreateReward(Guid playerId, IRewardable rewardable)
    {
        Debug.WriteLine($"Attempting to add/update reward for player ID: {playerId}");

        Rewards.AddOrUpdate(playerId,
            // Add value factory
            (id) =>
            {
                var newQueue = new ConcurrentQueue<IRewardable>();
                newQueue.Enqueue(rewardable);
                Debug.WriteLine($"Created new queue for player ID: {playerId}");
                return newQueue;
            },
            // Update value factory
            (id, queue) =>
            {
                queue.Enqueue(rewardable);
                Debug.WriteLine($"Updated queue for player ID: {playerId}");
                return queue;
            });

        Debug.WriteLine($"Operation complete for player ID: {playerId}. Queue count: {Rewards[playerId].Count}");
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
