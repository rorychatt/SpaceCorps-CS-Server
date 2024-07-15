using System.Collections.Concurrent;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Statistics;

public interface IStatisticsServer
{
    public void StorePlayersStats(ConcurrentDictionary<Guid, IPlayer> players);
        
}
