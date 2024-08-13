using SpaceCorpsServerShared;

namespace SpaceCorpsServerTests;
public class StatisticsServerTests
{
    [Fact]
    public void TestStatisticsServer_Start_No_Crash()
    {
        const int port = 3000;
        var server = new Server(port);

        var statisticsServer = server.StatisticsServer;
        Assert.NotNull(statisticsServer);
    }
}
