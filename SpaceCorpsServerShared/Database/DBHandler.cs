using System.Collections.Concurrent;
using MySql.Data.MySqlClient;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Database;
public class DBHandler : IDBHandler
{
    MySqlConnection connection = new();

    public DBHandler(MySqlConnection connection)
    {
        this.connection = connection;
    }
    public void CreateMissingTables()
    {
        throw new NotImplementedException();
    }

    public Dictionary<string, List<KeyValuePair<string, string>>> GetSchema()
    {
        var tables = new Dictionary<string, List<KeyValuePair<string, string>>>();

        var tableNames = new List<string>();
        using (var command = new MySqlCommand("SHOW TABLES", connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tableNames.Add(reader[0].ToString()!);
                }
            }
        }

        foreach (var tableName in tableNames)
        {
            var fields = new List<KeyValuePair<string, string>>();

            using (var commandDescribe = new MySqlCommand($"DESCRIBE {tableName}", connection))
            {
                using (var readerDescribe = commandDescribe.ExecuteReader())
                {
                    while (readerDescribe.Read())
                    {
                        var fieldName = readerDescribe[0].ToString();
                        var fieldType = readerDescribe[1].ToString();
                        fields.Add(new KeyValuePair<string, string>(fieldName!, fieldType!));
                    }
                }
            }

            tables.Add(tableName, fields);
        }

        return tables;

    }

    public void SetMySQLConnection(MySqlConnection connection)
    {
        this.connection = connection;
    }

    public void StorePlayersStats(ConcurrentDictionary<Guid, IPlayer> players)
    {
        throw new NotImplementedException();
    }
}
