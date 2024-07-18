namespace SpaceCorpsServerShared.Statistics;

public interface IRewardable : IReward
{
    public Guid PlayerId { get; init; }

}
