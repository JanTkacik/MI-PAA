using System;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblem.Model;

using DecompositionTable = System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<int, int>>;

namespace KnapsackProblem.Algorithms
{
    class DynamicByCostSolver : IKnapsackSolver
    {
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

            int maxCost = items.Sum(item => item.Cost);

            while (true)
            {
                int weight = GetValueFromDecompositionTable(decompositionTable, items, items.Count - 1, maxCost);
                if (weight <= problem.BagCapacity)
                {
                    return maxCost;
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
    }
}
