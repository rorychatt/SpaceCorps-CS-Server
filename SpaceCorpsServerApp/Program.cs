using Microsoft.Extensions.Logging;
using Moq;
using SpaceCorpsServerShared;

namespace SpaceCorpsServerApp;
class Program
{
    private static void Main(string[] args)
    {
        const int port = 4000;
        var loggerMock = new Mock<ILogger<Server>>();
        var server = new Server(loggerMock.Object, port);

        server.Start();
    }
}