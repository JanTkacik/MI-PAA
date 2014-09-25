using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                Dictionary<int, int> knownResults = null;
                Dictionary<int, Tuple<int, long>> bruteForceResults = new Dictionary<int, Tuple<int, long>>();
                Dictionary<int, Tuple<int, long>> costToRatioHeuristicsResults = new Dictionary<int, Tuple<int, long>>();
                
                if (options.ResultFiles != null)
                {
                    knownResults = parser.ParseResults(options.ResultFiles);
                }

                VerboseLog("Done.");

                IKnapsackSolver bruteForceSolver = new BruteForceSolver();
                IKnapsackSolver ratioHeuristicSolver = new RatioHeuristicSolver();

                VerboseLog("Calculation started");

                Stopwatch stopwatch = new Stopwatch();

                foreach (KnapsackProblemModel problem in knapsackProblemModels)
                {
                    VerboseLog("Solving problem:");
                    VerboseLog(problem);

                    int knownResult = -1;
                    if (knownResults != null)
                    {
                        knownResult = knownResults[problem.ProblemId];
                        VerboseLog("Result should be: " + knownResult);
                    }

                    if (options.BruteForce)
                    {
                        VerboseLog("Brute force solver ...");

                        stopwatch.Restart();
                        int result = bruteForceSolver.Solve(problem);
                        stopwatch.Stop();

                        bruteForceResults.Add(problem.ProblemId, new Tuple<int, long>(result, stopwatch.ElapsedTicks));

                        if (knownResult != -1 && result != knownResult)
                        {
                            Console.WriteLine("ERROR - Brute force algorithm not accurate for problem " + problem);
                            Environment.Exit(1);
                        }
                    }

                    if (options.CostToRatioHeuristics)
                    {
                        VerboseLog("Ratio heuristics solver ...");

                        stopwatch.Restart();
                        int result = ratioHeuristicSolver.Solve(problem);
                        stopwatch.Stop();

                        costToRatioHeuristicsResults.Add(problem.ProblemId, new Tuple<int, long>(result, stopwatch.ElapsedTicks));
                    }

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
