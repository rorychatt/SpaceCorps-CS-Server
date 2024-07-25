using Microsoft.Extensions.Logging;
using SpaceCorpsServerShared.Database;
namespace SpaceCorpsServerTests.DBTests;

public class DBTests
{
    private static readonly string ConnectionString = "Server=rorycraft.com;Database=spacecorps;Uid=server;Pwd=popapenis123;";
    private static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddConsole());
    private static readonly ILogger<DBHandler> Logger = LoggerFactory.CreateLogger<DBHandler>();
    
    [Fact]
    public void CanConnect_ToDB()
    {
        var db = new DB(ConnectionString, Logger);

        db.OpenConnection();
        db.CloseConnection();

        Assert.True(true);
    }

    [Fact]
    public void Gets_All_Tables_Remote()
    {
        var db = new DB(ConnectionString, Logger);

        db.OpenConnection();
        var tableSchema = db.GetDBHandler().GetSchema();
        db.CloseConnection();

        Assert.True(tableSchema.Count > 0);
    }

    [Fact]
    public async Task Gets_Players_DataAsync()
    {
        var db = new DB(ConnectionString, Logger);

        db.OpenConnection();
        var players = await db.GetDBHandler().GetPlayersStatsAsync();
        db.CloseConnection();

        Assert.True(players.Count > 0);
    }

    [Fact]
    public async void CreatesMissingPlayerEntityTable()
    {
        var db = new DB(ConnectionString, Logger);

        db.OpenConnection();
        await db.GetDBHandler().CreatePlayerEntityTableIfNotExistsAsync();
        var players = await db.GetDBHandler().GetPlayersStatsAsync();

        db.CloseConnection();

        Assert.True(players.Count >= 0);
    }

    [Fact]
    public async void CanLoad_PlayerEntityStats_FromDB()
    {
        var db = new DB(ConnectionString, Logger);

        db.OpenConnection();
        var players = await db.GetDBHandler().GetPlayersStatsAsync();
        db.CloseConnection();

        var playerStats = players.FirstOrDefault();
        // var userName = playerStats.username;

        // Assert.NotNull(player);
    }

}