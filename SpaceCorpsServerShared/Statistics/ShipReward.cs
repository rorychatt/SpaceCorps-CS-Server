namespace SpaceCorpsServerShared.Statistics;

public class ItemReward : IReward
{
    public Guid PlayerId { get; init; }
    public int ItemId { get; init; }
    public ItemReward(Guid playerId, int itemId)
    {
        PlayerId = playerId;
        ItemId = itemId;
    }
}