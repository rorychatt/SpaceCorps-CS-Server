using System.Collections.Concurrent;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Statistics;
public class StatisticsServer : IStatisticsServer
{
    public void StorePlayersStats(ConcurrentDictionary<Guid, IPlayer> players)
    {
        
    }
}
