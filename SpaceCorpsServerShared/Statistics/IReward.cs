namespace SpaceCorpsServerShared.Statistics;
public interface IReward
{
    public IReward GetAsReward(Guid playerId);
}
