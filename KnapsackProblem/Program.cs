using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AForge.Genetic;
using KnapsackProblem.Algorithms;
using KnapsackProblem.Configuration;
using KnapsackProblem.Helpers;
using KnapsackProblem.Model;

namespace KnapsackProblem
{
    class Program
    {
        private static readonly Options Options = new Options();

        static void Main(string[] args)
        {
            if (CommandLine.Parser.Default.ParseArguments(args, Options))
            {
                VerboseLog("Data parsing ...");

                DataParser parser = new DataParser();
                List<KnapsackProblemModel> knapsackProblemModels = parser.ParseProblem(Options.InputFiles);
                Dictionary<int, int> knownResults = null;
                Dictionary<int, Tuple<int, long>> bruteForceResults = new Dictionary<int, Tuple<int, long>>();
                Dictionary<int, Tuple<int, long>> costToRatioHeuristicsResults = new Dictionary<int, Tuple<int, long>>();
                Dictionary<int, Tuple<int, long>> branchAndBoundResults = new Dictionary<int, Tuple<int, long>>();
                Dictionary<int, Tuple<int, long>> dynamicByCostResults = new Dictionary<int, Tuple<int, long>>();
                Dictionary<int, Tuple<int, long>> fptasResults = new Dictionary<int, Tuple<int, long>>();
                Dictionary<int, Tuple<int, long>> geneticResults = new Dictionary<int, Tuple<int, long>>();

                if (Options.ResultFiles != null)
                {
                    knownResults = parser.ParseResults(Options.ResultFiles);
                }

                VerboseLog("Done.");

                IKnapsackSolver bruteForceSolver = new BruteForceSolver();
                IKnapsackSolver ratioHeuristicSolver = new RatioHeuristicSolver();
                IKnapsackSolver branchAndBoundSolver = new BranchAndBoundSolver();
                IKnapsackSolver dynamicByCostSolve = new DynamicByCostSolver();
                IKnapsackSolver fptasSolver = null;
                IKnapsackSolver geneticSolver = null;

                if (Options.FPTAS)
                {
                    fptasSolver = new FPTASSolver(Options.FPTASAccuracy);
                }
                if (Options.Genetics)
                {
                    ISelectionMethod selectionMethod = null;
                    switch (Options.SelectionMethod)
                    {
                        case "roulette": selectionMethod = new RouletteWheelSelection();
                            break;
                        case "rank" : selectionMethod = new RankSelection();
                            break;
                        case "elitary" : selectionMethod = new EliteSelection();
                            break;
                        default: Console.WriteLine("Wrong selection method for genetics");
                            break;
                    }

                    if (selectionMethod == null)
                    {
                        return;
                    }

                    geneticSolver = new GeneticSolver(Options.PopulationSize, Options.IterationsCount,selectionMethod, Options.MutationRate, Options.CrossoverRate, Options.RandomSelectionPortion, true);
                }


                VerboseLog("Solving JIT instance");
                KnapsackProblemModel jitProblem = new KnapsackProblemModel(-1, 100, new List<Item>
                {
                    new Item(18, 114, 0), new Item(42, 136, 1), new Item(88, 192, 2), new Item(3, 223, 3)
                });

                bruteForceSolver.Solve(jitProblem);
                ratioHeuristicSolver.Solve(jitProblem);
                branchAndBoundSolver.Solve(jitProblem);
                dynamicByCostSolve.Solve(jitProblem);

                if (fptasSolver != null)
                {
                    fptasSolver.Solve(jitProblem);
                }

                if (geneticSolver != null)
                {
                    geneticSolver.Solve(jitProblem);
                }

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

                    if (Options.BruteForce)
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

                    if (Options.BranchAndBound)
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

                    if (Options.DynamicByCost)
                    {
                        VerboseLog("Dynamic by cost solver ...");

                        stopwatch.Restart();
                        int result = dynamicByCostSolve.Solve(problem);
                        stopwatch.Stop();

                        dynamicByCostResults.Add(problem.ProblemId, new Tuple<int, long>(result, stopwatch.ElapsedTicks));

                        if (knownResult != -1 && result != knownResult)
                        {
                            Console.WriteLine("ERROR - Dynamic by cost algorithm not accurate for problem " + problem);
                            Environment.Exit(1);
                        }
                    }

                    if (Options.CostToRatioHeuristics)
                    {
                        VerboseLog("Ratio heuristics solver ...");

                        stopwatch.Restart();
                        int result = ratioHeuristicSolver.Solve(problem);
                        stopwatch.Stop();

                        costToRatioHeuristicsResults.Add(problem.ProblemId, new Tuple<int, long>(result, stopwatch.ElapsedTicks));
                    }

                    if (Options.FPTAS)
                    {
                        VerboseLog("FPTAS solver ...");
                        
                        if (fptasSolver != null)
                        {
                            stopwatch.Restart();
                            int result = fptasSolver.Solve(problem);
                            stopwatch.Stop();

                            fptasResults.Add(problem.ProblemId, new Tuple<int, long>(result, stopwatch.ElapsedTicks));
                        }
                    }

                    if (Options.Genetics)
                    {
                        VerboseLog("Genetics solver ...");

                        if (geneticSolver != null)
                        {
                            stopwatch.Restart();
                            int result = geneticSolver.Solve(problem);
                            stopwatch.Stop();

                            geneticResults.Add(problem.ProblemId, new Tuple<int, long>(result, stopwatch.ElapsedTicks));
                        }
                    }

                    VerboseLog("Problem solved.");
                }

                TextWriter reportWriter = Options.OutputFilePath == null ? Console.Out : new StreamWriter(Options.OutputFilePath);
                
                decimal frequency = Stopwatch.Frequency;

                reportWriter.Write("Problem ID;Items count");
                if (knownResults != null)
                {
                    reportWriter.Write(";Known result");
                }
                if (Options.BruteForce)
                {
                    reportWriter.Write(";Brute force result;Time [s]");
                    if (knownResults != null)
                    {
                        reportWriter.Write(";Relative error");
                    }
                }
                if (Options.CostToRatioHeuristics)
                {
                    reportWriter.Write(";Cost to weight ration heuristics result;Time [s]");
                    if (knownResults != null)
                    {
                        reportWriter.Write(";Relative error");
                    }
                }
                if (Options.BranchAndBound)
                {
                    reportWriter.Write(";Branch and bound result;Time [s]");
                    if (knownResults != null)
                    {
                        reportWriter.Write(";Relative error");
                    }
                }
                if (Options.DynamicByCost)
                {
                    reportWriter.Write(";Dynamic programming by cost result;Time [s]");
                    if (knownResults != null)
                    {
                        reportWriter.Write(";Relative error");
                    }
                }
                if (Options.FPTAS)
                {
                    reportWriter.Write(";FPTAS result;Time [s]");
                    if (knownResults != null)
                    {
                        reportWriter.Write(";Relative error;Max possible error");
                    }
                }
                if (Options.Genetics)
                {
                    reportWriter.Write(";Genetics result;Time [s]");
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
                    if (Options.BruteForce)
                    {
                        Tuple<int, long> bruteForceResult = bruteForceResults[problemId];
                        reportWriter.Write(";" + bruteForceResult.Item1 + ";" + bruteForceResult.Item2 / frequency);
                        if (knownResults != null)
                        {
                            reportWriter.Write(";" + CalculateRelativeError(knownResults[problemId], bruteForceResult.Item1));
                        }
                    }
                    if (Options.CostToRatioHeuristics)
                    {
                        Tuple<int, long> heuristicsResult = costToRatioHeuristicsResults[problemId];
                        reportWriter.Write(";" + heuristicsResult.Item1 + ";" + heuristicsResult.Item2 / frequency);
                        if (knownResults != null)
                        {
                            reportWriter.Write(";" + CalculateRelativeError(knownResults[problemId], heuristicsResult.Item1));
                        }
                    }
                    if (Options.BranchAndBound)
                    {
                        Tuple<int, long> heuristicsResult = branchAndBoundResults[problemId];
                        reportWriter.Write(";" + heuristicsResult.Item1 + ";" + heuristicsResult.Item2 / frequency);
                        if (knownResults != null)
                        {
                            reportWriter.Write(";" + CalculateRelativeError(knownResults[problemId], heuristicsResult.Item1));
                        }
                    }
                    if (Options.DynamicByCost)
                    {
                        Tuple<int, long> heuristicsResult = dynamicByCostResults[problemId];
                        reportWriter.Write(";" + heuristicsResult.Item1 + ";" + heuristicsResult.Item2 / frequency);
                        if (knownResults != null)
                        {
                            reportWriter.Write(";" + CalculateRelativeError(knownResults[problemId], heuristicsResult.Item1));
                        }
                    }
                    if (Options.FPTAS)
                    {
                        Tuple<int, long> heuristicsResult = fptasResults[problemId];
                        reportWriter.Write(";" + heuristicsResult.Item1 + ";" + heuristicsResult.Item2 / frequency);
                        if (knownResults != null)
                        {
                            reportWriter.Write(";" + CalculateRelativeError(knownResults[problemId], heuristicsResult.Item1));
                            reportWriter.Write(";" + ((FPTASSolver)fptasSolver).GetMaximumError(problem));
                        }
                    }
                    if (Options.Genetics)
                    {
                        Tuple<int, long> heuristicsResult = geneticResults[problemId];
                        reportWriter.Write(";" + heuristicsResult.Item1 + ";" + heuristicsResult.Item2 / frequency);
                        if (knownResults != null)
                        {
                            reportWriter.Write(";" + CalculateRelativeError(knownResults[problemId], heuristicsResult.Item1));
                        }
                    }
                    reportWriter.WriteLine();
                }

                if (Options.Genetics)
                {
                    decimal totalTime = 0;
                    decimal totalError = 0;

                    foreach (KnapsackProblemModel problem in knapsackProblemModels)
                    {
                        int problemId = problem.ProblemId;
                        Tuple<int, long> result = geneticResults[problemId];
                        totalTime += (result.Item2/frequency);
                        totalError += CalculateRelativeError(knownResults[problemId], result.Item1);
                    }

                    decimal averageError = totalError/knapsackProblemModels.Count;

                    reportWriter.WriteLine("Aggregate results");
                    reportWriter.WriteLine("Aggregate time");
                    reportWriter.WriteLine(totalTime);
                    reportWriter.WriteLine("Average error");
                    reportWriter.WriteLine(averageError);
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
            if (Options.Verbose)
            {
                Console.WriteLine(data.ToString());
            }
        }
    }
}
