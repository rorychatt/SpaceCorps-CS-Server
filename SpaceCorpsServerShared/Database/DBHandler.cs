using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerShared.Database;
public class DBHandler(MySqlConnection connection, ILogger<DBHandler> logger) : IDbHandler
{
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
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tableNames.Add(reader[0].ToString()!);
            }
        }

        foreach (var tableName in tableNames)
        {
            var fields = new List<KeyValuePair<string, string>>();

            using (var commandDescribe = new MySqlCommand($"DESCRIBE {tableName}", connection))
            {
                using var readerDescribe = commandDescribe.ExecuteReader();
                while (readerDescribe.Read())
                {
                    var fieldName = readerDescribe[0].ToString();
                    var fieldType = readerDescribe[1].ToString();
                    fields.Add(new KeyValuePair<string, string>(fieldName!, fieldType!));
                }
            }

            tables.Add(tableName, fields);
        }

        return tables;

    }

    // This method should not be used outside of this class.
    // TODO: Isolate this method later.
    public async Task ExecuteSqlCommandAsync(string sqlCommand)
    {
        try
        {
            using var command = new MySqlCommand(sqlCommand, connection);
            await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while executing the SQL command: {SqlCommand}", sqlCommand);
            throw;
        }
    }

    public void SetMySqlConnection(MySqlConnection connection1)
    {
        connection = connection1;
    }

    public async Task<Dictionary<string, IPlayerEntityDto>> GetPlayersStatsAsync()
    {
        var playersStats = new Dictionary<string, IPlayerEntityDto>();
        const string query = "SELECT * FROM playerEntity";

        await using var command = new MySqlCommand(query, connection);
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            Dictionary<string, object> parameters = new();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                parameters.Add(reader.GetName(i), reader.GetValue(i));
            }

            var playerStat = new PlayerEntityDto(parameters);

            playersStats.Add(playerStat.Username, playerStat);
        }
        return playersStats;
    }
    public async Task CreatePlayerEntityTableIfNotExistsAsync()
    {
        const string createTableQuery = """
                                                CREATE TABLE IF NOT EXISTS playerEntity (
                                                    username VARCHAR(255),
                                                    mapName VARCHAR(255),
                                                    company VARCHAR(255),
                                                    positionX FLOAT,
                                                    positionY FLOAT,
                                                    credits INT,
                                                    thulium INT,
                                                    experience INT,
                                                    honor INT,
                                                    level INT
                                                );
                                        """;
        await ExecuteSqlCommandAsync(createTableQuery);
    }
}