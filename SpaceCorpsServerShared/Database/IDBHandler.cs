using System.Collections.Concurrent;
using MySql.Data.MySqlClient;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Database;

public interface IDBHandler
{
    public void CreateMissingTables();
    public Task ExecuteSqlCommandAsync(string sqlCommand);
    public void SetMySQLConnection(MySqlConnection connection);
    public Dictionary<string, List<KeyValuePair<string, string>>> GetSchema();

}