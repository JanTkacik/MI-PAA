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

            foreach (Item t in items)
            {
                bag.InsertItem(t);
                if (!bag.AcceptableWeight())
                {
                    bag.RemoveItem(t);
                }
            }

            return bag.ItemsCost();
        }
    }
}
