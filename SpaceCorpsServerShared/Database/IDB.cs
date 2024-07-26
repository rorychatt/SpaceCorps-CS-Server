using MySql.Data.MySqlClient;

namespace SpaceCorpsServerShared.Database;

public interface IDb
{
    public void SetDbHandler(IDbHandler dbHandler);
    public IDbHandler GetDbHandler();
    public void OpenConnection();
    public void CloseConnection();
}
