using Microsoft.Extensions.Logging;
using SpaceCorpsServerShared.Database;
namespace SpaceCorpsServerTests.DBTests;

public class DBTests
{

    static readonly string connectionString = "Server=rorycraft.com;Database=spacecorps;Uid=server;Pwd=popapenis123;";
    static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    static readonly ILogger<DBHandler> logger = loggerFactory.CreateLogger<DBHandler>();


    [Fact]
    public void CanConnect_ToDB()
    {
        var db = new DB(connectionString, logger);

        db.OpenConnection();
        db.CloseConnection();

        Assert.True(true);
    }

    [Fact]
    public void Gets_All_Tables_Remote()
    {
        var db = new DB(connectionString, logger);

        db.OpenConnection();
        var tableSchema = db.GetDBHandler().GetSchema();
        db.CloseConnection();

        Assert.True(tableSchema.Count > 0);
    }

    [Fact]
    public async Task Gets_Players_DataAsync()
    {
        var db = new DB(connectionString, logger);

        db.OpenConnection();
        var players = await db.GetDBHandler().GetPlayersStatsAsync();
        db.CloseConnection();

        Assert.True(players.Count > 0);
    }




}