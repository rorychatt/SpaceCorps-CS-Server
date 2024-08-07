using SpaceCorpsServerShared.Entity;
using SpaceCorpsServerShared.Items;
using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerShared.Players;

public interface IPlayer : IEntity
{
    public void AddItem(IItem item);
    public void RemoveItem(IItem item);
    public void ClearInventory();
    public IInventory GetInventory();
    public IStats GetStats();
    void UpdateStats(IRewardable rewardable);
}
