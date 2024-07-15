using SpaceCorpsServerShared.Database;

namespace SpaceCorpsServerTests.DBTests;

public class DBTests
{
    static readonly string connectionString = "Server=rorycraft.com;Database=spacecorps;Uid=server;Pwd=popapenis123;"; [Fact]
    public void CanConnect_ToDB()
    {
        var db = new DB(connectionString);

        db.OpenConnection();
        db.CloseConnection();

        Assert.True(true);
    }

    [Fact]
    public void Gets_All_Tables()
    {
        var db = new DB(connectionString);

        db.OpenConnection();
        var tables = db.GetDBHandler().GetAllTables();
        db.CloseConnection();

        Assert.True(tables.Count > 0);
    }


}