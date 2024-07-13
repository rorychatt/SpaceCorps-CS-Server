using System.Net.WebSockets;
using SpaceCorpsServerShared;

namespace SpaceCorpsServerTests;

public class ServerTests
{
    [Fact]
    public async Task TestWebSocketConnection()
    {
        // Arrange
        Server server = new Server();
        Task serverTask = server.Start(new string[0]);

        // Act
        ClientWebSocket client = new ClientWebSocket();
        await client.ConnectAsync(new Uri("ws://localhost:5000/"), CancellationToken.None);

        // Assert
        Assert.Equal(WebSocketState.Open, client.State);

        // Clean up
        await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Test complete", CancellationToken.None);
        await serverTask;
    }
}