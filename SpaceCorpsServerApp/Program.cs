using SpaceCorpsServerShared;

namespace SpaceCorpsServerApp;
class Program
{
    private static void Main(string[] args)
    {
        const int port = 4000;
        var server = new Server(port);

        server.Start();
    }
}