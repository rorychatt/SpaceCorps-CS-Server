using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Statistics;
public class StatsReward : IReward
{
    public Guid PlayerId { get; init; }
    public Stats Statistics { get; init; }
    public StatsReward(Guid playerId, Stats statistics)
    {
        PlayerId = playerId;
        Statistics = statistics;
    }
}