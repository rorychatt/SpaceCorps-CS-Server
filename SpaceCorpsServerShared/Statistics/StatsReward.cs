using System.Reflection.Metadata.Ecma335;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Statistics;
public class StatsReward : IStatsReward
{
    public Guid PlayerId { get; init; }
    public Stats Statistics { get; init; }
    public StatsReward(Guid playerId, Stats statistics)
    {
        PlayerId = playerId;
        Statistics = statistics;
    }

    public IReward GetAsReward(Guid playerId)
    {
        return new StatsReward(PlayerId, Statistics);
    }
}