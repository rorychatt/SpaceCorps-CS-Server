
using System.Numerics;
using SpaceCorpsServerShared.Items;
using SpaceCorpsServerShared.Statistics;

namespace SpaceCorpsServerShared.Players
{
    public class Player : IPlayer
    {
        public Guid Id { get; }
        public Vector3 Position { get; set; }
        private Inventory Inventory { get; } = new Inventory();

        private Stats Stats { get; } = new Stats();

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

        public void UpdateStats(IRewardable rewardable)
        {
            var rewardableType = rewardable.GetType();
            var statisticsProperty = rewardableType.GetProperty("Statistics");
            var itemProperty = rewardableType.GetProperty("Item");

            if (statisticsProperty != null)
            {
                var statistics = statisticsProperty.GetValue(rewardable);
                if (statistics is IStats stats)
                {
                    Stats.AddCredits(stats.GetCredits());
                    Stats.AddThulium(stats.GetThulium());
                    Stats.AddExperience(stats.GetExperience());
                    Stats.AddHonor(stats.GetHonor());
                }
            }
            else if (itemProperty != null)
            {
                var item = itemProperty.GetValue(rewardable);
                if (item is IItem iItem)
                {
                    AddItem(iItem);
                }
            }
            else
            {
                throw new ArgumentException("Unknown rewardable type");
            }
        }
    }
}