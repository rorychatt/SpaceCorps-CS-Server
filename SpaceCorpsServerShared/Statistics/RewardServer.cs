namespace SpaceCorpsServerShared.Statistics;
public class RewardServer : IRewardServer
{
    public Dictionary<Guid, List<IRewardable>> Rewards => [];
    public void CreateReward(Guid playerId, IRewardable rewardable)
    {
        if (!Rewards.TryGetValue(playerId, out List<IRewardable>? value))
        {
            value = [];
            Rewards[playerId] = value;
        }

        value.Add(rewardable);
    }
    public List<IRewardable> GetRewardsForUser(Guid playerId)
    {
        if (!Rewards.TryGetValue(playerId, out List<IRewardable>? value))
        {
            return [];
        }

        return value;
    }

    public IRewardable? HandleRewardsForUser(Guid playerId)
    {
        if (!Rewards.TryGetValue(playerId, out List<IRewardable>? value))
        {
            return null;
        }

        var reward = value[0];
        value.RemoveAt(0);
        return reward;
    }
}
