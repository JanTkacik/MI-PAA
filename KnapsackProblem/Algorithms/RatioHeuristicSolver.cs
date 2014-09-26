using System.Collections.Generic;
using System.Linq;
using KnapsackProblem.Model;

namespace KnapsackProblem.Algorithms
{
    class RatioHeuristicSolver : IKnapsackSolver
    {
        public int Solve(KnapsackProblemModel problem)
        {
            List<Item> items = problem.Items.ToList();
            items.Sort(Item.CostToWeightRatioComparerDescending);

            Bag bag = new Bag(problem.BagCapacity);

            int lastAdded = -1;
            while (bag.AcceptableWeight())
            {
                lastAdded++;
                if (lastAdded >= items.Count)
                {
                    break;
                }
                bag.InsertItem(items[lastAdded]);
            }
            
            if (!bag.AcceptableWeight())
            {
                bag.RemoveItem(items[lastAdded]);
            }

            return bag.ItemsCost();
        }
    }
}
