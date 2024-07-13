using System.Numerics;
using SpaceCorpsServerShared.Entity;

namespace SpaceCorpsServerTests;

public class EntityTests
{
    [Fact]
    public void TestPlayer_Id_ReturnsGuid()
    {
        var player = new Player();
        Assert.NotEqual(Guid.Empty, player.Id);
    }

    [Fact]
    public void TestPlayer_Position_ReturnsVector3()
    {
        var player = new Player();
        Assert.Equal(Vector3.Zero, player.Position);
    }

    [Fact]
    public void TestPlayer_Position_Set()
    {
        var player = new Player();
        var newPosition = new Vector3(1, 2, 3);
        player.Position = newPosition;
        Assert.Equal(newPosition, player.Position);
    }

}