using System;

namespace KnapsackProblem.Model
{
    public class Item : IEquatable<Item>
    {
        public int Weight { get; private set; }
        public int Cost { get; private set; }
        public int ItemId { get; private set; }

        public Item(int weight, int cost, int itemId) 
        {
            ItemId = itemId;
            Weight = weight;
            Cost = cost;
        }

        public bool Equals(Item other)
        {
            return Weight == other.Weight && Cost == other.Cost && ItemId == other.ItemId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Item && Equals((Item) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Weight;
                hashCode = (hashCode*397) ^ Cost;
                hashCode = (hashCode*397) ^ ItemId;
                return hashCode;
            }
        }

        public override string ToString()
        {
            return string.Format("ItemId: {2}, Weight: {0}, Cost: {1}", Weight, Cost, ItemId);
        }
    }
}
