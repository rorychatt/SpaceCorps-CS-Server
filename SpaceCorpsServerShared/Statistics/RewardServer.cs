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

    public void CreateReward(Guid playerId, IRewardable rewardable)
    {
        if (!Rewards.TryGetValue(playerId, out ConcurrentQueue<IRewardable>? value))
        {
            value = new ConcurrentQueue<IRewardable>();
            Rewards[playerId] = value;
        }

        value.Enqueue(rewardable);
    }
    public ConcurrentQueue<IRewardable> GetRewardsForUser(Guid playerId)
    {
        return Rewards[playerId];
    }

    public IRewardable? HandleRewardsForUser(Guid playerId)
    {
        if (!Rewards.TryGetValue(playerId, out ConcurrentQueue<IRewardable>? value))
        {
            return null;
        }
        if (value.TryDequeue(out var reward))
        {
            return reward;
        }
        return null;
    }
}
