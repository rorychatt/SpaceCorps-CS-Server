using System.Collections.Concurrent;
using MySql.Data.MySqlClient;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Database;

public interface IDBHandler
{
    public void StorePlayersStats(ConcurrentDictionary<Guid, IPlayer> players);
    public void CreateMissingTables();
    public void SetMySQLConnection(MySqlConnection connection);
    public Dictionary<string, List<KeyValuePair<string, string>>> GetAllTables();

}