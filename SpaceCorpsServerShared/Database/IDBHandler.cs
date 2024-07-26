using System.Collections.Concurrent;
using MySql.Data.MySqlClient;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Database;

public interface IDbHandler
{
    public void CreateMissingTables();
    public Task ExecuteSqlCommandAsync(string sqlCommand);
    public void SetMySqlConnection(MySqlConnection connection);
    public Dictionary<string, List<KeyValuePair<string, string>>> GetSchema();
    Task<Dictionary<string, IPlayerEntityDto>> GetPlayersStatsAsync();
    public Task CreatePlayerEntityTableIfNotExistsAsync();
}