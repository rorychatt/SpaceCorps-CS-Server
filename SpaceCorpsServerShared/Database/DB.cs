using System;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace SpaceCorpsServerShared.Database;
public class DB : IDB
{
    private MySqlConnection connection;
    private IDBHandler dbHandler;
    private readonly ILogger<DBHandler> logger;

    public DB(string connectionString, ILogger<DBHandler> logger)
    {
        this.logger = logger;
        connection = new MySqlConnection(connectionString);
        dbHandler = new DBHandler(connection, logger);
    }

    public void OpenConnection()
    {
        try
        {
            connection.Open();
        }
        catch (Exception ex)
        {
            logger.LogInformation("Cannot open connection to database. Error: {Message}", ex.Message);
        }
    }

    public void CloseConnection()
    {
        connection.Close();
    }

    public void SetDBHandler(IDBHandler _dbHandler)
    {
        dbHandler = _dbHandler;
    }

    public IDBHandler GetDBHandler()
    {
        return dbHandler;
    }
}
