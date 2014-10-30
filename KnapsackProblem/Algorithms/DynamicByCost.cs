using System;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblem.Model;

namespace KnapsackProblem.Algorithms
{
    class DynamicByCost : IKnapsackSolver
    {
        public int Solve(KnapsackProblemModel problem)
        {
            List<Item> items = problem.Items.ToList();
            items.Sort((item, item1) => item.ItemId - item1.ItemId);
            //       Price,         ItemId, Weight
            Dictionary<int, Dictionary<int, int>> decompositionTable = new Dictionary<int, Dictionary<int, int>>
                {
                    //W(0,0) = 0
                    {0, new Dictionary<int, int>{{-1, 0}}}
                };

            foreach (Item item in items)
            {
                int itemId = item.ItemId;
                int itemCost = item.Cost;

                List<int> costs = decompositionTable.Keys.ToList();

                foreach (int cost in costs)
                {
                    Dictionary<int, int> tableRow = decompositionTable[cost];
                    
                    //NOT IN THE BAG
                    tableRow.Add(itemId, tableRow[itemId - 1]);
                    
                    //IN THE BAG
                    int weight = tableRow[itemId];
                    int newCost = cost + itemCost;
                    int newWeight = weight + item.Weight;

                    if (!decompositionTable.ContainsKey(newCost))
                    {
                        decompositionTable.Add(newCost, new Dictionary<int, int>());
                    }
                    Dictionary<int, int> row = decompositionTable[newCost];
                    if (row.ContainsKey(itemId))
                    {
                        row[itemId] = Math.Min(newWeight, row[itemId]);
                    }
                    else
                    {
                        row.Add(itemId, newWeight);
                    }
                }
            }

            List<int> costsReverse = decompositionTable.Keys.ToList();
            costsReverse.Sort((i, i1) => i1 - i);
            int lastItemId = items.Last().ItemId;

            foreach (int cost in costsReverse)
            {
                int weight = decompositionTable[cost][lastItemId];
                if (weight <= problem.BagCapacity)
                {
                    return cost;
                }
            }

            return 0;
        }
    }
}
