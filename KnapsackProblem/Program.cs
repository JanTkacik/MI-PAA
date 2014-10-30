using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                List<KnapsackProblemModel> knapsackProblemModels = parser.ParseProblem(options.InputFiles);
                Dictionary<int, int> knownResults = null;
                Dictionary<int, Tuple<int, long>> bruteForceResults = new Dictionary<int, Tuple<int, long>>();
                Dictionary<int, Tuple<int, long>> costToRatioHeuristicsResults = new Dictionary<int, Tuple<int, long>>();
                Dictionary<int, Tuple<int, long>> branchAndBoundResults = new Dictionary<int, Tuple<int, long>>();
                Dictionary<int, Tuple<int, long>> dynamicByCostResults = new Dictionary<int, Tuple<int, long>>();

                if (options.ResultFiles != null)
                {
                    knownResults = parser.ParseResults(options.ResultFiles);
                }

                VerboseLog("Done.");

                IKnapsackSolver bruteForceSolver = new BruteForceSolver();
                IKnapsackSolver ratioHeuristicSolver = new RatioHeuristicSolver();
                IKnapsackSolver branchAndBoundSolver = new BranchAndBoundSolver();
                IKnapsackSolver dynamicByCost = new DynamicByCost();

                VerboseLog("Solving JIT instance");
                KnapsackProblemModel JITProblem = new KnapsackProblemModel(9000, 100, new List<Item>
                {
                    new Item(18, 114, 0), new Item(42, 136, 1), new Item(88, 192, 2), new Item(3, 223, 3)
                });

                bruteForceSolver.Solve(JITProblem);
                ratioHeuristicSolver.Solve(JITProblem);
                branchAndBoundSolver.Solve(JITProblem);
                dynamicByCost.Solve(JITProblem);

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

                    if (options.BranchAndBound)
                    {
                        VerboseLog("Branch and bound solver ...");

                        stopwatch.Restart();
                        int result = branchAndBoundSolver.Solve(problem);
                        stopwatch.Stop();

                        branchAndBoundResults.Add(problem.ProblemId, new Tuple<int, long>(result, stopwatch.ElapsedTicks));

                        if (knownResult != -1 && result != knownResult)
                        {
                            Console.WriteLine("ERROR - Branch and bound algorithm not accurate for problem " + problem);
                            Environment.Exit(1);
                        }
                    }

                    if (options.DynamicByCost)
                    {
                        VerboseLog("Dynamic by cost solver ...");

                        stopwatch.Restart();
                        int result = dynamicByCost.Solve(problem);
                        stopwatch.Stop();

                        dynamicByCostResults.Add(problem.ProblemId, new Tuple<int, long>(result, stopwatch.ElapsedTicks));

                        if (knownResult != -1 && result != knownResult)
                        {
                            Console.WriteLine("ERROR - Dynamic by cost algorithm not accurate for problem " + problem);
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

                TextWriter reportWriter;

                if (options.OutputFilePath == null)
                {
                    reportWriter = Console.Out;
                }
                else
                {
                    reportWriter = new StreamWriter(options.OutputFilePath);
                }
                
                decimal frequency = Stopwatch.Frequency;

                reportWriter.Write("Problem ID;Items count");
                if (knownResults != null)
                {
                    reportWriter.Write(";Known result");
                }
                if (options.BruteForce)
                {
                    reportWriter.Write(";Brute force result;Time [s]");
                    if (knownResults != null)
                    {
                        reportWriter.Write(";Relative error");
                    }
                }
                if (options.CostToRatioHeuristics)
                {
                    reportWriter.Write(";Cost to weight ration heuristics result;Time [s]");
                    if (knownResults != null)
                    {
                        reportWriter.Write(";Relative error");
                    }
                }
                reportWriter.WriteLine();

                foreach (KnapsackProblemModel problem in knapsackProblemModels)
                {
                    var problemId = problem.ProblemId;
                    reportWriter.Write(problemId);
                    reportWriter.Write(";" + problem.Items.Count);
                    if (knownResults != null)
                    {
                        reportWriter.Write(";" + knownResults[problemId]);
                    }
                    if (options.BruteForce)
                    {
                        Tuple<int, long> bruteForceResult = bruteForceResults[problemId];
                        reportWriter.Write(";" + bruteForceResult.Item1 + ";" + bruteForceResult.Item2 / frequency);
                        if (knownResults != null)
                        {
                            reportWriter.Write(";" + CalculateRelativeError(knownResults[problemId], bruteForceResult.Item1));
                        }
                    }
                    if (options.CostToRatioHeuristics)
                    {
                        Tuple<int, long> heuristicsResult = costToRatioHeuristicsResults[problemId];
                        reportWriter.Write(";" + heuristicsResult.Item1 + ";" + heuristicsResult.Item2 / frequency);
                        if (knownResults != null)
                        {
                            reportWriter.Write(";" + CalculateRelativeError(knownResults[problemId], heuristicsResult.Item1));
                        }
                    }
                    reportWriter.WriteLine();
                }
            }
            else
            {
                Environment.Exit(1);
            }

            Environment.Exit(0);
        }

        private static decimal CalculateRelativeError(int knownResult, int realResult)
        {
            int diff = Math.Abs(knownResult - realResult);
            return (diff)/((decimal)knownResult);
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
