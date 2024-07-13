using System.Net.WebSockets;
using Microsoft.Extensions.Logging;
using Moq;
using SpaceCorpsServerShared;

namespace SpaceCorpsServerTests;

public class ServerTests
{
    private readonly Mock<ILogger<Server>> loggerMock;
    private readonly Server server;

    public ServerTests()
    {
        loggerMock = new Mock<ILogger<Server>>();
        server = new Server(loggerMock.Object, 5000);
    }

    [Fact]
    public async Task TestServerStartLogsMessage()
    {
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, 5000);

        var cts = new CancellationTokenSource();
        cts.CancelAfter(1000);

        var startTask = server.StartAsync(cts.Token);

        await Task.WhenAny(startTask, Task.Delay(500));

        loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == "Server started."),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    
}