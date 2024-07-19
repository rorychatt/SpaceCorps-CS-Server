namespace SpaceCorpsServerShared.Statistics;

public class ItemReward : IRewardable
{
    public Guid PlayerId { get; init; }
    public int ItemId { get; init; }
    public ItemReward(Guid playerId, int itemId)
    {
        PlayerId = playerId;
        ItemId = itemId;
    }

    public IReward GetAsReward(Guid playerId)
    {
        return new ItemReward(playerId, ItemId);
    }
}