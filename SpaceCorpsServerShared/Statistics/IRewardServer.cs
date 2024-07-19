namespace SpaceCorpsServerShared.Statistics;

public interface IRewardServer
{
    public Dictionary<Guid, List<IRewardable>> GetRewards();
    void CreateReward(Guid playerId, IRewardable rewardable);
    IRewardable? HandleRewardsForUser(Guid playerId);
    List<IRewardable> GetRewardsForUser(Guid playerId);
}