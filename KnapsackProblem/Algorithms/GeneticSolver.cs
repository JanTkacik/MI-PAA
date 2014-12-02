using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AForge.Genetic;
using KnapsackProblem.Model;

namespace KnapsackProblem.Algorithms
{
    public class GeneticSolver : IKnapsackSolver
    {
        private readonly int _populationSize;
        private readonly int _iterationsCount;
        private readonly ISelectionMethod _selectionMethod;
        private readonly double _mutationRate;
        private readonly double _crossoverRate;
        private readonly bool _logging;

        public GeneticSolver(int populationSize, int iterationsCount, ISelectionMethod selectionMethod, double mutationRate, double crossoverRate, bool logging)
        {
            _populationSize = populationSize;
            _iterationsCount = iterationsCount;
            _selectionMethod = selectionMethod;
            _mutationRate = mutationRate;
            _crossoverRate = crossoverRate;
            _logging = logging;
        }

        public int Solve(KnapsackProblemModel problem)
        {
            Population population = new Population(
                _populationSize, 
                new BinaryChromosome(problem.Items.Count), 
                new KnapsackFitnessFunction(
                    new Bag(problem.BagCapacity), 
                    problem.Items.ToList()), 
                _selectionMethod) {MutationRate = _mutationRate, CrossoverRate = _crossoverRate};

            StreamWriter log = null;
            if (_logging)
            {
                string path = "GeneticsLog_" + problem.ProblemId;
                if(File.Exists(path))
                {
                    log = File.AppendText(path);
                }
                else
                {
                    log = File.CreateText(path);
                }
                log.WriteLine("Population size," + _populationSize);
                log.WriteLine("Crossover rate," + _crossoverRate);
                log.WriteLine("Mutation rate," + _mutationRate);
                log.WriteLine("Selection method," + _selectionMethod);
                log.Write("Iteration,FitnessMax,FitnessAvg");
            }

            for (int i = 0; i < _iterationsCount; i++)
            {
                population.RunEpoch();
                if (log != null)
                {
                    log.WriteLine(i + "," + population.FitnessMax + "," + population.FitnessAvg);
                }
            }

            return Convert.ToInt32(population.FitnessMax);
        }

        private class KnapsackFitnessFunction : IFitnessFunction
        {
            private static readonly ulong[] _testBitsMask = new ulong[]
                {
                    0x0000000000000001,
                    0x0000000000000002,
                    0x0000000000000004,
                    0x0000000000000008,
                    0x0000000000000010,
                    0x0000000000000020,
                    0x0000000000000040,
                    0x0000000000000080,
                    0x0000000000000100,
                    0x0000000000000200,
                    0x0000000000000400,
                    0x0000000000000800,
                    0x0000000000001000,
                    0x0000000000002000,
                    0x0000000000004000,
                    0x0000000000008000,
                    0x0000000000010000,
                    0x0000000000020000,
                    0x0000000000040000,
                    0x0000000000080000,
                    0x0000000000100000,
                    0x0000000000200000,
                    0x0000000000400000,
                    0x0000000000800000,
                    0x0000000001000000,
                    0x0000000002000000,
                    0x0000000004000000,
                    0x0000000008000000,
                    0x0000000010000000,
                    0x0000000020000000,
                    0x0000000040000000,
                    0x0000000080000000,
                    0x0000000100000000,
                    0x0000000200000000,
                    0x0000000400000000,
                    0x0000000800000000,
                    0x0000001000000000,
                    0x0000002000000000,
                    0x0000004000000000,
                    0x0000008000000000,
                    0x0000010000000000,
                    0x0000020000000000,
                    0x0000040000000000,
                    0x0000080000000000,
                    0x0000100000000000,
                    0x0000200000000000,
                    0x0000400000000000,
                    0x0000800000000000,
                    0x0001000000000000,
                    0x0002000000000000,
                    0x0004000000000000,
                    0x0008000000000000,
                    0x0010000000000000,
                    0x0020000000000000,
                    0x0040000000000000,
                    0x0080000000000000,
                    0x0100000000000000,
                    0x0200000000000000,
                    0x0400000000000000,
                    0x0800000000000000,
                    0x1000000000000000,
                    0x2000000000000000,
                    0x4000000000000000,
                    0x8000000000000000
                };
            private readonly Bag _bag;
            private readonly List<Item> _items;

            public KnapsackFitnessFunction(Bag bag, List<Item> items)
            {
                _bag = bag;
                _items = items;
            }

            public double Evaluate(IChromosome chromosome)
            {
                BinaryChromosome knapsackConfiguration = (BinaryChromosome) chromosome;

                int length = knapsackConfiguration.Length;
                ulong value = knapsackConfiguration.Value;
                for (int i = 0; i < length; i++)
                {
                    if ((value & _testBitsMask[i]) == 0)
                    {
                        _bag.RemoveItem(_items[i]);
                    }
                    else
                    {
                        _bag.InsertItem(_items[i]);
                    }
                }

                if (_bag.AcceptableWeight())
                {
                    return _bag.ItemsCost();
                }
                return 0;
            }
        }
    }
}
