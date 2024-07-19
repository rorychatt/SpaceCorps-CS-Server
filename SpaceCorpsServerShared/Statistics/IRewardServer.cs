namespace SpaceCorpsServerShared.Statistics;

public interface IRewardServer
{
    Dictionary<Guid, List<IRewardable>> Rewards{get;}
    void CreateReward(Guid playerId, IRewardable rewardable);
    IRewardable? HandleRewardsForUser(Guid playerId);
    List<IRewardable> GetRewardsForUser(Guid playerId);
}