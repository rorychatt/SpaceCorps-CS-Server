using Microsoft.Extensions.Logging;
using Moq;
using SpaceCorpsServerShared;

namespace SpaceCorpsServerTests;
public class StatisticsServerTests
{
    [Fact]
    public void TestStatisticsServer_Start_No_Crash()
    {
        const int port = 3000;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);

        var statisticsServer = server.GetStatisticsServer();
        Assert.NotNull(statisticsServer);
    }
}
