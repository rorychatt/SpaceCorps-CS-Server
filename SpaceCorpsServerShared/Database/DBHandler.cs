using System.Collections.Concurrent;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Database;
public class DBHandler : IDBHandler
{
    public void StorePlayersStats(ConcurrentDictionary<Guid, IPlayer> players)
    {
        throw new NotImplementedException();
    }
}
