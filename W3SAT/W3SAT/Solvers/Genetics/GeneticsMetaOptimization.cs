using System;
using System.Collections;
using System.Collections.Generic;
using AForge.Genetic;
using GridOptimizer;
using W3SAT.Model;

namespace W3SAT.Solvers.Genetics
{
    public class GeneticsMetaOptimization : IOptimizationProblem 
    {
        private readonly List<Formula> _problems;

        public GeneticsMetaOptimization(List<Formula> problems)
        {
            _problems = problems;
        }

        public int DimensionsCount { get { return 3; } }
        public double GetMinForDimension(int dimension)
        {
            return 0;
        }

        public double GetMaxForDimension(int dimension)
        {
            return 1;
        }

        public double GetStepForDimension(int dimension)
        {
            return 0.25;
        }

        public double GetStepChangeForDimension(int dimension)
        {
            return 0.1;
        }

        public double Evaluate(Configuration configuration)
        {
            GeneticsSolver geneticsSolver = new GeneticsSolver(
                30, 100, new EliteSelection(), 
                configuration.GetValueForDimension(0), 
                configuration.GetValueForDimension(1), 
                configuration.GetValueForDimension(2), 
                true, false);

            int sumOfSolutions = 0;
            foreach (Formula problem in _problems)
            {
                try
                {
                    Tuple<int, BitArray> solution = geneticsSolver.Solve(problem);
                    sumOfSolutions += solution.Item1;
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception");   
                }
            }
            Console.WriteLine("Testing: {0},{1},{2}\t\t\t{3}", configuration.GetValueForDimension(0), configuration.GetValueForDimension(1), configuration.GetValueForDimension(2), sumOfSolutions);
            return 1 / (double)sumOfSolutions;
        }
    }
}
