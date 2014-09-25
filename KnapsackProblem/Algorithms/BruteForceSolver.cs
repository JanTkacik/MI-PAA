using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblem.Model;

namespace KnapsackProblem.Algorithms
{
    class BruteForceSolver : IKnapsackSolver
    {
        public int Solve(KnapsackProblemModel problem)
        {
            Bag bag = new Bag(problem.BagCapacity);

            List<Item> items = problem.Items.ToList();

            int count = items.Count;
            BitArray possibility = new BitArray(count);

            int bestItemsCost = 0;

            while (true)
            {
                for (int i = 0; i < count; i++)
                {
                    //NEXT BIT WILL BE INVERTED
                    bool next = !possibility[i];
                    possibility[i] = next;

                    //IF NEW VALUE IS TRUE INSERT ITEM - IF FALSE REMOVE IT
                    if (next)
                    {
                        bag.InsertItem(items[i]);
                    }
                    else
                    {
                        bag.RemoveItem(items[i]);
                    }

                    //ONE IS SET - END
                    if (next)
                    {
                        break;
                    }
                }

                if (bag.IsEmpty())
                {
                    break;
                }

                if (bag.AcceptableWeight())
                {
                    int itemsCost = bag.ItemsCost();
                    if (itemsCost > bestItemsCost)
                    {
                        bestItemsCost = itemsCost;
                    }
                }
            }
            
            return bestItemsCost;
        }
    }
}
