
namespace SpaceCorpsServerShared.Item;

public class ShipItem : IItem
{
    public int ItemId {get; init;}

    public ShipItem()
    {
        ItemId = _getItemIdFromDB();
    }

    private int _getItemIdFromDB()
    {
        return 1;
    }
}