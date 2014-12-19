using System;
using System.Collections.Generic;
using System.Linq;
using W3SAT.Model;

namespace W3SAT.InstanceGenerator
{
    public static class InstanceGenerator
    {
        private static readonly Random Rand = new Random(DateTime.Now.Millisecond);

        public static Formula GenerateInstance(int variableCount, int clausuleCount, int minWeight = 0, int maxWeight = 100)
        {
            List<WeightedVariable> randomVariables = new List<WeightedVariable>();
            for (int i = 0; i < variableCount; i++)
            {
                randomVariables.Add(new WeightedVariable(i, Rand.Next(minWeight, maxWeight)));
            }

            List<Clausule> clausules = new List<Clausule>();
            for (int i = 0; i < clausuleCount; i++)
            {
                clausules.Add(GenerateClausule(variableCount, randomVariables));
            }

            return new Formula(clausules, variableCount);
        }

        private static Clausule GenerateClausule(int variableCount, List<WeightedVariable> randomVariables)
        {
            HashSet<int> randomVariablesChoice = new HashSet<int>();
            while (randomVariablesChoice.Count < 3)
            {
                randomVariablesChoice.Add(Rand.Next(variableCount));
            }

            bool modifier1 = (Rand.NextDouble() < 0.5);
            bool modifier2 = (Rand.NextDouble() < 0.5);
            bool modifier3 = (Rand.NextDouble() < 0.5);

            List<int> indexes = randomVariablesChoice.ToList();

            return new Clausule(modifier1, randomVariables[indexes[0]], modifier2, randomVariables[indexes[1]], modifier3, randomVariables[indexes[2]]);
        }
    }
}
