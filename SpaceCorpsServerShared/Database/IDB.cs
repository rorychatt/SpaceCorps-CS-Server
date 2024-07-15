using MySql.Data.MySqlClient;

namespace SpaceCorpsServerShared.Database;

public interface IDB
{
    public void SetDBHandler(IDBHandler dbHandler);
    public IDBHandler GetDBHandler();
    public void OpenConnection();
    public void CloseConnection();
}
