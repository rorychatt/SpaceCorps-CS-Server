using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerTests;
public class StatisticsServerTests
{
    [Fact]
    public void TestStatisticsServer_Start_No_Crash()
    {
        var port = 3000;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);

        IStatisticsServer statisticsServer = server.GetStatisticsServer();
        Assert.NotNull(statisticsServer);
    }
}
