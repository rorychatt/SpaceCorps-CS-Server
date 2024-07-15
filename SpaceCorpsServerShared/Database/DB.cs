using System;
using MySql.Data.MySqlClient;

namespace SpaceCorpsServerShared.Database;
public class DB : IDB
{
    private MySqlConnection connection;
    private IDBHandler dbHandler;

    public DB(string connectionString)
    {
        connection = new MySqlConnection(connectionString);
        dbHandler = new DBHandler(connection);
    }

    public void OpenConnection()
    {
        try
        {
            connection.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Cannot open connection to database. Error: " + ex.Message);
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
