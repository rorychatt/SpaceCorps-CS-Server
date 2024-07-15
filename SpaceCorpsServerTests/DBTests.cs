using SpaceCorpsServerShared.Database;

namespace SpaceCorpsServerTests.DBTests;

public class DBTests
{
    static readonly string connectionString = "Server=rorycraft.com;Database=spacecorps;Uid=server;Pwd=popapenis123;"; [Fact]
    public void CanConnect_ToDB()
    {
        var db = new DB(connectionString);
        var dbHandler = new DBHandler();
        db.SetDBHandler(dbHandler);

        db.OpenConnection();
        db.CloseConnection();

        Assert.True(true);
    }

    
}