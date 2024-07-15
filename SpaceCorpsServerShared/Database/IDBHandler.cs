using System.Collections.Concurrent;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Database;

public interface IDBHandler
{
    public void StorePlayersStats(ConcurrentDictionary<Guid, IPlayer> players);
        
}