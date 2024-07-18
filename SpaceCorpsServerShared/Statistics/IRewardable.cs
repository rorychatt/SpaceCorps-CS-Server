namespace SpaceCorpsServerShared.Statistics;

public interface IRewardable
{
    public IReward GetAsReward(Guid playerId);
}