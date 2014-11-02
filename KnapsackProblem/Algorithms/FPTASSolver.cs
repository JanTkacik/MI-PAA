using System;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblem.Model;

using DecompositionTable = System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<int, int>>;

namespace KnapsackProblem.Algorithms
{
    public class FPTASSolver : IKnapsackSolver
    {
        private readonly int _bitesShift;

        public FPTASSolver(int bitesShift)
        {
            _bitesShift = bitesShift;
        }

        public int Solve(KnapsackProblemModel problem)
        {
            List<Item> items = problem.Items.ToList();
            items.Sort((item, item1) => item.ItemId - item1.ItemId);
            //       Price,         ItemId, Weight
            DecompositionTable decompositionTable = new DecompositionTable
                    {
                        //W(0,0) = 0
                        {0, new Dictionary<int, int>{{-1, 0}}}
                    };
            
            List<Item> itemsShifted = new List<Item>();
            //FPTAS SHIFT
            foreach (var item in items)
            {
                itemsShifted.Add(new Item(item.Weight, item.Cost >> _bitesShift, item.ItemId));
            }

            int maxCost = itemsShifted.Sum(item => item.Cost);

            while (true)
            {
                int weight = GetValueFromDecompositionTable(decompositionTable, itemsShifted, itemsShifted.Count - 1, maxCost);
                if (weight <= problem.BagCapacity)
                {
                    return maxCost << _bitesShift;
                }
                maxCost--;
            }
        }

        public int GetValueFromDecompositionTable(DecompositionTable table, List<Item> items, int itemIndex, int cost)
        {
            if (itemIndex == -1)
            {
                if (cost > 0)
                {
                    return int.MaxValue - items.Max(item => item.Weight);
                }
                return 0;
            }

            Item actItem = items[itemIndex];

            if (table.ContainsKey(cost))
            {
                if (table[cost].ContainsKey(itemIndex))
                {
                    return table[cost][itemIndex];
                }
            }
            else
            {
                table.Add(cost, new Dictionary<int, int>());
            }

            int firstCandidate = GetValueFromDecompositionTable(table, items, itemIndex - 1, cost);
            int secondCandidate = GetValueFromDecompositionTable(table, items, itemIndex - 1, cost - actItem.Cost) + actItem.Weight;

            int min = Math.Min(firstCandidate, secondCandidate);

            table[cost].Add(itemIndex, min);

            return min;
        }

        public double GetMaximumError(KnapsackProblemModel problem)
        {
            var n = problem.Items.Count;
            var cMax = problem.Items.Sum(item => item.Cost);

            return (n * Math.Pow(2, _bitesShift))/cMax;
        }
    }
}
