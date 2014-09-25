using System;
using System.Collections.Generic;
using System.Text;

namespace KnapsackProblem.Model
{
    public class KnapsackProblemModel : IEquatable<KnapsackProblemModel>
    {
        private readonly int _problemId;
        public int BagCapacity { get; private set; }
        public HashSet<Item> Items { get; private set; }

        public KnapsackProblemModel(int problemId, int bagCapacity, IEnumerable<Item> items)
        {
            BagCapacity = bagCapacity;
            Items = new HashSet<Item>(items);
            _problemId = problemId;
        }

        public bool Equals(KnapsackProblemModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _problemId == other._problemId && BagCapacity == other.BagCapacity && Items.SetEquals(other.Items);
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
                hashCode = (hashCode*397) ^ BagCapacity;
                hashCode = (hashCode*397) ^ (Items != null ? Items.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            StringBuilder items = new StringBuilder();
            foreach (Item item in Items)
            {
                items.AppendLine(item.ToString());
            }
            return string.Format("ProblemId: {0}, BagCapacity: {1}, Items: {2}", _problemId, BagCapacity, items);
        }
    }
}
