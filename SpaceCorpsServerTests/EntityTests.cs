using System.Numerics;
using SpaceCorpsServerShared.Item;
using SpaceCorpsServerShared.Players;

namespace SpaceCorpsServerTests;

public class EntityTests
{

}
public class PlayerTests
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

    [Fact]
    public void TestPlayer_AddCredits()
    {
        var player = new Player();
        player.GetStats().SetCredits(0);
        var oldCredits = player.GetStats().GetCredits();
        player.GetStats().AddCredits(100);
        var newCredits = player.GetStats().GetCredits();
        Assert.Equal(100, player.GetStats().GetCredits());
        Assert.Equal(oldCredits + 100, newCredits);
    }

    [Fact]
    public void TestPlayer_AddNegativeCredits()
    {
        var player = new Player();
        player.GetStats().SetCredits(100);
        var oldCredits = player.GetStats().GetCredits();
        player.GetStats().AddCredits(-100);
        var newCredits = player.GetStats().GetCredits();
        Assert.Equal(0, player.GetStats().GetCredits());
        Assert.Equal(oldCredits - 100, newCredits);
    }

    [Fact]
    public void TestPlayer_AddThulium()
    {
        var player = new Player();
        player.GetStats().SetThulium(0);
        var oldThulium = player.GetStats().GetThulium();
        player.GetStats().AddThulium(100);
        var newThulium = player.GetStats().GetThulium();
        Assert.Equal(100, player.GetStats().GetThulium());
        Assert.Equal(oldThulium + 100, newThulium);
    }

    [Fact]
    public void TestPlayer_AddNegativeThulium()
    {
        var player = new Player();
        player.GetStats().SetThulium(100);
        var oldThulium = player.GetStats().GetThulium();
        player.GetStats().AddThulium(-100);
        var newThulium = player.GetStats().GetThulium();
        Assert.Equal(0, player.GetStats().GetThulium());
        Assert.Equal(oldThulium - 100, newThulium);
    }

    [Fact]
    public void TestPlayer_AddHonor()
    {
        var player = new Player();
        player.GetStats().SetHonor(0);
        var oldHonor = player.GetStats().GetHonor();
        player.GetStats().AddHonor(100);
        var newHonor = player.GetStats().GetHonor();
        Assert.Equal(100, player.GetStats().GetHonor());
        Assert.Equal(oldHonor + 100, newHonor);
    }

    [Fact]
    public void TestPlayer_AddNegativeHonor()
    {
        var player = new Player();
        player.GetStats().SetHonor(100);
        var oldHonor = player.GetStats().GetHonor();
        player.GetStats().AddHonor(-100);
        var newHonor = player.GetStats().GetHonor();
        Assert.Equal(0, player.GetStats().GetHonor());
        Assert.Equal(oldHonor - 100, newHonor);
    }

    [Fact]
    public void TestPlayer_AddExperience()
    {
        var player = new Player();
        player.GetStats().SetExperience(0);
        var oldExperience = player.GetStats().GetExperience();
        player.GetStats().AddExperience(100);
        var newExperience = player.GetStats().GetExperience();
        Assert.Equal(100, player.GetStats().GetExperience());
        Assert.Equal(oldExperience + 100, newExperience);
    }

    [Fact]
    public void TestPlayer_AddNegativeExperience()
    {
        var player = new Player();
        player.GetStats().SetExperience(100);
        var oldExperience = player.GetStats().GetExperience();
        player.GetStats().AddExperience(-100);
        var newExperience = player.GetStats().GetExperience();
        Assert.Equal(0, player.GetStats().GetExperience());
        Assert.Equal(oldExperience - 100, newExperience);
    }



}