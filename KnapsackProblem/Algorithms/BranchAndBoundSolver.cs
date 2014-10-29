using System.Collections.Generic;
using System.Linq;
using KnapsackProblem.Model;

namespace KnapsackProblem.Algorithms
{
    class BranchAndBoundSolver : IKnapsackSolver
    {
        public int Solve(KnapsackProblemModel problem)
        {
            Bag bag = new Bag(problem.BagCapacity);

            List<Item> items = problem.Items.ToList();

            return RecursiveSolver(bag, items, 0);
        }

        public int RecursiveSolver(Bag bag, List<Item> items, int bestKnown)
        {
            if (items.Count > 0)
            {
                int bestItemsCost = bestKnown;

                List<Item> recList = new List<Item>(items);
                recList.RemoveAt(0);

                int sum = recList.Sum(item => item.Cost) + bag.ItemsCost();
                if (sum >= bestKnown)
                {
                    bestItemsCost = RecursiveSolver(bag, recList, bestKnown);
                }
                bag.InsertItem(items[0]);
                if (bag.AcceptableWeight())
                {
                    int itemsCost = bag.ItemsCost();
                    if (itemsCost > bestItemsCost)
                    {
                        bestItemsCost = itemsCost;
                    }
                }
                recList = new List<Item>(items);
                recList.Remove(items[0]);
                sum = recList.Sum(item => item.Cost) + bag.ItemsCost();
                if (sum >= bestItemsCost)
                {
                    bestItemsCost = RecursiveSolver(bag, recList, bestItemsCost);
                }
                bag.RemoveItem(items[0]);
                return bestItemsCost;
            }
            return bestKnown;
        }
    }
}
