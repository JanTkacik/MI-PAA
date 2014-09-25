using System;
using System.Collections.Generic;
using System.Text;

namespace KnapsackProblem.Model
{
    public class KnapsackProblemModel : IEquatable<KnapsackProblemModel>
    {
        private readonly int _problemId;
        private readonly int _bagCapacity;
        private readonly HashSet<Item> _items;

        public KnapsackProblemModel(int problemId, int bagCapacity, IEnumerable<Item> items)
        {
            _bagCapacity = bagCapacity;
            _items = new HashSet<Item>(items);
            _problemId = problemId;
        }

        public bool Equals(KnapsackProblemModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _problemId == other._problemId && _bagCapacity == other._bagCapacity && _items.SetEquals(other._items);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((KnapsackProblemModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _problemId;
                hashCode = (hashCode*397) ^ _bagCapacity;
                hashCode = (hashCode*397) ^ (_items != null ? _items.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            StringBuilder items = new StringBuilder();
            foreach (Item item in _items)
            {
                items.AppendLine(item.ToString());
            }
            return string.Format("ProblemId: {0}, BagCapacity: {1}, Items: {2}", _problemId, _bagCapacity, items);
        }
    }
}
