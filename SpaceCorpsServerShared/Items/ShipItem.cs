
using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerShared.Item;

public class ShipItem : IItem, IRewardable
{
    public int ItemId { get; init; }
    public Guid PlayerId { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

    public ShipItem()
    {
        ItemId = _getItemIdFromDB();
    }

    private int _getItemIdFromDB()
    {
        return 1;
    }

    public IReward GetAsReward(Guid playerId)
    {
        return new ItemReward(playerId, ItemId);
    }
}