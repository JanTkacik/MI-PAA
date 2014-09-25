using System.Collections.Generic;
using System.Linq;

namespace KnapsackProblem.Model
{
    public class Bag
    {
        public int Capacity { get; private set; }
        private readonly HashSet<Item> _items;

        public Bag(int capacity)
        {
            Capacity = capacity;
            _items = new HashSet<Item>();
        }

        public void InsertItem(Item item)
        {
            _items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }

        public bool AcceptableWeight()
        {
            return ItemsWeight() <= Capacity;
        }

        public int ItemsWeight()
        {
            return _items.Sum(item => item.Weight);
        }

        public int ItemsCost()
        {
            return _items.Sum(item => item.Cost);
        }

        public bool IsEmpty()
        {
            return _items.Count == 0;
        }
    }
}
