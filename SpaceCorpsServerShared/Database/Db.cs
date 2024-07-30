using System;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace SpaceCorpsServerShared.Database;
public class Db : IDb
{
    private readonly MySqlConnection _connection;
    private IDbHandler _dbHandler;
    private readonly ILogger<DBHandler> _logger;

    public Db(string connectionString, ILogger<DBHandler> logger)
    {
        this._logger = logger;
        _connection = new MySqlConnection(connectionString);
        _dbHandler = new DBHandler(_connection, logger);
    }

    public void OpenConnection()
    {
        try
        {
            _connection.Open();
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Cannot open connection to database. Error: {Message}", ex.Message);
        }
    }

    public void CloseConnection()
    {
        _connection.Close();
    }

    public void SetDbHandler(IDbHandler dbHandler)
    {
        this._dbHandler = dbHandler;
    }

    public IDbHandler GetDbHandler()
    {
        return _dbHandler;
    }
}
