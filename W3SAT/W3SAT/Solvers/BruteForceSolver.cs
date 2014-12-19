using System;
using System.Collections;
using W3SAT.Model;

namespace W3SAT.Solvers
{
    public class BruteForceSolver : IW3SATSolver
    {
        public Tuple<int, BitArray> Solve(Formula problem)
        {
            int variablesCount = problem.VariablesCount;
            BitArray possibility = new BitArray(variablesCount);

            int bestWeight = -1;
            BitArray bestSolution = null;

            do
            {
                if (problem.IsSatisfied(possibility))
                {
                    int weight = problem.GetWeight(possibility);
                    if (weight > bestWeight)
                    {
                        bestWeight = weight;
                        bestSolution = new BitArray(possibility);
                    }
                }
            } while (Increment(possibility));

            return new Tuple<int, BitArray>(bestWeight, bestSolution);
        }

        public bool Increment(BitArray bArray)
        {
            for (int i = 0; i < bArray.Length; i++)
            {
                bool previous = bArray[i];
                bArray[i] = !previous;
                if (!previous)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
