using System;
using System.Collections.Generic;
using KnapsackProblem.Algorithms;
using KnapsackProblem.Configuration;
using KnapsackProblem.Helpers;
using KnapsackProblem.Model;

namespace KnapsackProblem
{
    class Program
    {
        private static readonly Options options = new Options();

        static void Main(string[] args)
        {
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                VerboseLog("Data parsing ...");

                DataParser parser = new DataParser();
                IEnumerable<KnapsackProblemModel> knapsackProblemModels = parser.ParseProblem(options.InputFiles);

                VerboseLog("Done.");

                IKnapsackSolver bruteForceSolver = new BruteForceSolver();
                IKnapsackSolver ratioHeuristicSolver = new RatioHeuristicSolver();

                VerboseLog("Calculation started");

                foreach (KnapsackProblemModel problem in knapsackProblemModels)
                {
                    VerboseLog("Solving problem:");
                    VerboseLog(problem);
                    VerboseLog("Brute force solver ...");
                    bruteForceSolver.Solve(problem);
                    VerboseLog("Ratio heuristics solver ...");
                    ratioHeuristicSolver.Solve(problem);
                    VerboseLog("Problem solved.");
                }
            }
            else
            {
                Environment.Exit(1);
            }

            Environment.Exit(0);
        }

        private static void VerboseLog(object data)
        {
            if (options.Verbose)
            {
                Console.WriteLine(data.ToString());
            }
        }
    }
}
