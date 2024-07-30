using Microsoft.Extensions.Logging;
using SpaceCorpsServerShared.Database;

namespace SpaceCorpsServerTests;

public class DbTests
{
    private const string ConnectionString = "Server=rorycraft.com;Database=spacecorps;Uid=server;Pwd=popapenis123;";
    private static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddConsole());
    private static readonly ILogger<DbHandler> Logger = LoggerFactory.CreateLogger<DbHandler>();
    
    [Fact]
    public void CanConnect_ToDB()
    {
        var db = new Db(ConnectionString, Logger);

        db.OpenConnection();
        db.CloseConnection();

        Assert.True(true);
    }

    [Fact]
    public void Gets_All_Tables_Remote()
    {
        var db = new Db(ConnectionString, Logger);

        db.OpenConnection();
        var tableSchema = db.GetDbHandler().GetSchema();
        db.CloseConnection();

        Assert.True(tableSchema.Count > 0);
    }

    [Fact]
    public async Task Gets_Players_DataAsync()
    {
        var db = new Db(ConnectionString, Logger);

        db.OpenConnection();
        var players = await db.GetDbHandler().GetPlayersStatsAsync();
        db.CloseConnection();

        Assert.True(players.Count > 0);
    }

    [Fact]
    public async void CreatesMissingPlayerEntityTable()
    {
        var db = new Db(ConnectionString, Logger);

        db.OpenConnection();
        await db.GetDbHandler().CreatePlayerEntityTableIfNotExistsAsync();
        var players = await db.GetDbHandler().GetPlayersStatsAsync();

        db.CloseConnection();

        Assert.True(players.Count >= 0);
    }

    [Fact]
    public async void CanLoad_PlayerEntityStats_FromDB()
    {
        var db = new Db(ConnectionString, Logger);

        db.OpenConnection();
        var players = await db.GetDbHandler().GetPlayersStatsAsync();
        db.CloseConnection();

        var playerStats = players.FirstOrDefault();
        // var userName = playerStats.username;

        // Assert.NotNull(player);
    }

}