using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerShared.Items;

public class ShipItem : IItem, IRewardable
{
    public int ItemId { get; init; } = _getItemIdFromDB();
    public Guid PlayerId { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

    private static int _getItemIdFromDB()
    {
        return 1;
    }

    public IReward GetAsReward(Guid playerId)
    {
        return new ItemReward(playerId, ItemId);
    }
}