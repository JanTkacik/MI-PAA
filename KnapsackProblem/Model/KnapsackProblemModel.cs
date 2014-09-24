using System.Collections.Generic;

namespace KnapsackProblem.Model
{
    public class KnapsackProblemModel
    {
        private readonly int _problemId;
        private readonly int _bagCapacity;
        private readonly IEnumerable<Item> _items;

        public KnapsackProblemModel(int problemId, int bagCapacity, IEnumerable<Item> items)
        {
            _bagCapacity = bagCapacity;
            _items = items;
            _problemId = problemId;
        }
    }
}
