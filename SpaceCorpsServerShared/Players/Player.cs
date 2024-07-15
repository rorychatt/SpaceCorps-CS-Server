
using System.Numerics;
using SpaceCorpsServerShared.Item;

namespace SpaceCorpsServerShared.Players
{
    public class Player : IPlayer
    {
        public Guid Id { get; }
        public Vector3 Position { get; set; }
        private IInventory Inventory { get; } = new Inventory();

        private IStats Stats { get; } = new Stats();

        public Player()
        {
            Id = Guid.NewGuid();
        }

        public Player(Guid id)
        {
            Id = id;
        }

        public void AddItem(IItem item)
        {
            Inventory.AddItem(item);
        }

        public void ClearInventory()
        {
            Inventory.ClearInventory();
        }

        public void RemoveItem(IItem item)
        {
            Inventory.RemoveItem(item);
        }

        public IInventory GetInventory()
        {
            return Inventory;
        }

        public IStats GetStats()
        {
            return Stats;
        }
    }
}