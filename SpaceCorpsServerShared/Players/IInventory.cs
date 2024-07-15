using SpaceCorpsServerShared.Item;

namespace SpaceCorpsServerShared.Players;
public interface IInventory
{
    public void AddItem(IItem item);
    public void RemoveItem(IItem item);
    public void ClearInventory();
    public void ResetInventory();
    public Dictionary<int, IItem> GetContents();
}
