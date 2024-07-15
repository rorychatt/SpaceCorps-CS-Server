using System.Numerics;
using SpaceCorpsServerShared.Item;
using SpaceCorpsServerShared.Players;

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

    [Fact]
    public void TestPlayer_Inventory_AddItem()
    {
        var player = new Player();
        var item = new ShipItem();
        player.AddItem(item);
        Assert.Contains(item.ItemId, player.GetInventory().GetContents().Keys);
    }

    [Fact]
    public void TestPlayer_Inventory_RemoveItem()
    {
        var player = new Player();
        var item = new ShipItem();
        player.AddItem(item);
        player.RemoveItem(item);
        Assert.DoesNotContain(item.ItemId, player.GetInventory().GetContents().Keys);
    }

    [Fact]
    public void TestPlayer_Inventory_RemoveItem_NotFound()
    {
        var player = new Player();
        var item = new ShipItem();
        player.RemoveItem(item);
        Assert.DoesNotContain(item.ItemId, player.GetInventory().GetContents().Keys);
    }

    [Fact]
    public void TestPlayer_Inventory_RemoveItem_Empty()
    {
        var player = new Player();
        var item = new ShipItem();
        player.AddItem(item);
        player.RemoveItem(item);
        player.RemoveItem(item);
        Assert.DoesNotContain(item.ItemId, player.GetInventory().GetContents().Keys);
    }

}