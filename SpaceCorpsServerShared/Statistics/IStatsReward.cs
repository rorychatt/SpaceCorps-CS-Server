namespace SpaceCorpsServerShared.Statistics;
public interface IStatsReward : IRewardable
{
    public new IReward GetAsReward(Guid playerId);
}
