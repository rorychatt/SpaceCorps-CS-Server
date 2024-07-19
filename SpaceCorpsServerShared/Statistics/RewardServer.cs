using System.Collections.Concurrent;

namespace SpaceCorpsServerShared.Statistics;
public class RewardServer : IRewardServer
{
    public ConcurrentDictionary<Guid, ConcurrentQueue<IRewardable>> Rewards;

    public RewardServer()
    {
        Rewards = [];
    }
    public ConcurrentDictionary<Guid, ConcurrentQueue<IRewardable>> GetRewards()
    {
        return Rewards;
    }

    public async Task CreateReward(Guid playerId, IRewardable rewardable)
    {
        await Task.Run(() =>
        {
            if (!Rewards.TryGetValue(playerId, out ConcurrentQueue<IRewardable>? value))
            {
                value = new ConcurrentQueue<IRewardable>();
                lock (Rewards)
                {
                    if (!Rewards.ContainsKey(playerId))
                    {
                        Rewards[playerId] = value;
                    }
                }
            }
            value.Enqueue(rewardable);
        });
    }
    public ConcurrentQueue<IRewardable> GetRewardsForUser(Guid playerId)
    {
        return Rewards[playerId];
    }

    public async Task<IRewardable?> HandleRewardsForUser(Guid playerId)
    {
        return await Task.Run(() =>
        {
            if (Rewards.TryGetValue(playerId, out ConcurrentQueue<IRewardable>? value))
            {
                if (value.TryDequeue(out IRewardable? reward))
                {
                    return reward;
                }
            }
            return null;
        });
    }
}
