using System;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblem.Model;

using DecompositionTable = System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<int, int>>;

namespace KnapsackProblem.Algorithms
{
    public class FPTASSolver : IKnapsackSolver
    {
        private readonly double _precision;

        public FPTASSolver(double precision)
        {
            _precision = precision;
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

            DecompositionTable result = new DecompositionTable
                {
                    {0, new Dictionary<int, int>{{-1, -1}}}
                };
            
            List<Item> itemsShifted = new List<Item>();
            
            //FPTAS SHIFT
            int bitesShift = GetBitesShift(problem);
            foreach (var item in items)
            {
                itemsShifted.Add(new Item(item.Weight, item.Cost >> bitesShift, item.ItemId));
            }

            int maxCost = itemsShifted.Sum(item => item.Cost);

            
            int maxItemId = itemsShifted.Count - 1;
            while (true)
            {
                int weight = GetValueFromDecompositionTable(decompositionTable, itemsShifted, maxItemId, maxCost, result);
                if (weight <= problem.BagCapacity)
                {
                    int realCost = 0;
                    int actualCost = maxCost;

                    for (int i = maxItemId; i >= 0; i--)
                    {
                        if (result[actualCost][i] == 1)
                        {
                            int realItemCost = items[i].Cost;
                            int shiftedCost = itemsShifted[i].Cost;
                            realCost += realItemCost;
                            actualCost -= shiftedCost;
                        }
                    }

                    return realCost;
                }
                maxCost--;
            }
        }

        public int GetValueFromDecompositionTable(DecompositionTable table, List<Item> items, int itemIndex, int cost, DecompositionTable result)
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
                result.Add(cost, new Dictionary<int, int>());
            }

            int firstCandidate = GetValueFromDecompositionTable(table, items, itemIndex - 1, cost, result);
            int secondCandidate = GetValueFromDecompositionTable(table, items, itemIndex - 1, cost - actItem.Cost, result) + actItem.Weight;

            int min;

            if (firstCandidate <= secondCandidate)
            {
                min = firstCandidate;
                result[cost].Add(itemIndex, -1);
            }
            else
            {
                min = secondCandidate;
                result[cost].Add(itemIndex, 1);
            }
            
            table[cost].Add(itemIndex, min);

            return min;
        }

        public double GetMaximumError(KnapsackProblemModel problem)
        {
            var n = problem.Items.Count;            
            var cMax = problem.Items.Max(item => item.Cost);

            return (n * Math.Pow(2, GetBitesShift(problem)))/cMax;
        }

        private int GetBitesShift(KnapsackProblemModel problem)
        {
            int n = problem.Items.Count;
            int cMax = problem.Items.Max(item => item.Cost);

            double bitesShift = Math.Log(((_precision*cMax)/n), 2);

            return (int)Math.Ceiling(bitesShift);
        }
    }
}
