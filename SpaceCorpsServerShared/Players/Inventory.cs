using SpaceCorpsServerShared.Item;

namespace SpaceCorpsServerShared.Players;

public class Inventory : IInventory
{
    private Dictionary<int, IItem> Contents { get; } = [];

    public void AddItem(IItem item)
    {
        Contents.Add(item.ItemId, item);
    }

    public void ClearInventory()
    {
        Contents.Clear();
    }

    public void RemoveItem(IItem item)
    {
        Contents.Remove(item.ItemId);
    }

    public void ResetInventory()
    {
        ClearInventory();
    }

    public Dictionary<int, IItem> GetContents()
    {
        return Contents;
    }
}
