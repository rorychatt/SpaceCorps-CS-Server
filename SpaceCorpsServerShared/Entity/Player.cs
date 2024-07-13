
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using SpaceCorpsServerShared.Item;

namespace SpaceCorpsServerShared.Entity
{
    public class Player : IEntity
    {
        public Guid Id { get; init; }
        public Vector3 Position { get; set; }
        public Dictionary<int, IItem> Inventory { get; } = new Dictionary<int, IItem>();

        public Player()
        {
            Id = Guid.NewGuid();
            _loadDefaultSettings();
        }
        private void _loadDefaultSettings()
        {
            Position = Vector3.Zero;
        }

        public void AddItem(IItem item)
        {
            Inventory.Add(item.ItemId, item);
        }

        public void RemoveItem(IItem item)
        {
            Inventory.Remove(item.ItemId);
        }
    }
}