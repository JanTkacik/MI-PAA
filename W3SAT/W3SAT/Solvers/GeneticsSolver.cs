using System;
using System.Collections;
using System.IO;
using AForge.Genetic;
using W3SAT.Model;
using W3SAT.Solvers.Genetics;
using Population = W3SAT.Solvers.Genetics.Population;

namespace W3SAT.Solvers
{
    public class GeneticsSolver : IW3SATSolver
    {
        private readonly int _populationSize;
        private readonly int _iterationsCount;
        private readonly ISelectionMethod _selectionMethod;
        private readonly double _mutationRate;
        private readonly double _crossoverRate;
        private readonly double _randomSelectionPortion;
        private readonly bool _diversityCheck;
        private readonly bool _logging;

        public GeneticsSolver(int populationSize, int iterationsCount, ISelectionMethod selectionMethod, double mutationRate, double crossoverRate, double randomSelectionPortion, bool diversityCheck, bool logging)
        {
            _populationSize = populationSize;
            _iterationsCount = iterationsCount;
            _selectionMethod = selectionMethod;
            _mutationRate = mutationRate;
            _crossoverRate = crossoverRate;
            _randomSelectionPortion = randomSelectionPortion;
            _diversityCheck = diversityCheck;
            _logging = logging;
        }

        public Tuple<int, BitArray> Solve(Formula problem)
        {
            int BestSolution = 0;
            BitArray BestSolutionArray = new BitArray(problem.VariablesCount);

            Population population = new Population(
                _populationSize,
                new BitArrayChromosome(problem.VariablesCount), 
                new W3SATFitnessFunction(problem), 
                _selectionMethod) { MutationRate = _mutationRate, CrossoverRate = _crossoverRate, RandomSelectionPortion = _randomSelectionPortion, AutoShuffling = true };

            StreamWriter log = null;
            if (_logging)
            {
                string path = "GeneticsLog_" + problem.ID;
                if (File.Exists(path))
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
                log.WriteLine("Random selection portion," + _randomSelectionPortion);
                log.WriteLine("Selection method," + _selectionMethod);
                log.WriteLine("Diversity check," + _diversityCheck);
                log.WriteLine("Iteration,FitnessMax,FitnessAvg");
            }

            for (int i = 0; i < _iterationsCount; i++)
            {
                population.RunEpoch();
                if (population.FitnessMax > BestSolution)
                {
                    BestSolution = Convert.ToInt32(population.FitnessMax);
                    BestSolutionArray = new BitArray(((BitArrayChromosome)population.BestChromosome).Value);
                }
                if (log != null)
                {
                    log.WriteLine(i + "," + population.FitnessMax + "," + population.FitnessAvg);
                }
                if (_diversityCheck)
                {
                    if ((population.FitnessMax - population.FitnessAvg) < (population.FitnessMax * 0.07))
                    {
                        IChromosome bestChromosome = population.BestChromosome;
                        population.Regenerate();
                        population.AddChromosome(bestChromosome);
                    }
                }
            }

            if (log != null)
            {
                log.Close();
            }

            if (BestSolution > 100)
            {
                return new Tuple<int, BitArray>(BestSolution - 100, BestSolutionArray);
            }
            return new Tuple<int, BitArray>(0, BestSolutionArray);
        }
    }
}
