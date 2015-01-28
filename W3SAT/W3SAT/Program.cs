using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GridOptimizer.TestProblems;
using W3SAT.Model;
using W3SAT.Solvers;

namespace W3SAT
{
    class Program
    {
        static void Main(string[] args)
        {
            //////GENERATION OF DATA
            ////for (int variablesCount = 10; variablesCount <= 100; variablesCount += 10)
            ////{
            ////    for (int i = 0; i < 10; i++)
            ////    {
            ////        double ratio = 2 + (i*0.5);
            ////        int clausulesCount = Convert.ToInt32(ratio*variablesCount);

            ////        StreamWriter writer = File.CreateText("C:\\Users\\jan.tkacik\\Documents\\GitHub\\MI-PAA\\Data\\W3SAT\\Ratio_" + ratio + "_VariablesCount_" + variablesCount + ".dimacs");

            ////        for (int j = 0; j < 100; j++)
            ////        {
            ////            Formula problem = InstanceGenerator.InstanceGenerator.GenerateInstance(variablesCount, clausulesCount);
            ////            writer.WriteLine("PROBLEM ID = " + j);
            ////            writer.WriteLine(problem.ToString());
            ////        }
            ////        writer.Close();
            ////        Console.WriteLine("Done - " + variablesCount + " - " + ratio);
            ////    }
            ////}
            
            ////LOAD DATA 
            //for (int variablesCount = 10; variablesCount <= 100; variablesCount += 10)
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        double ratio = 2 + (i * 0.5);

            //        Dictionary<int, Formula> formulas = new Dictionary<int, Formula>();
            //        string[] allLines = File.ReadAllLines("C:\\Users\\jan.tkacik\\Documents\\GitHub\\MI-PAA\\Data\\W3SAT\\Ratio_" + ratio + "_VariablesCount_" + variablesCount + ".dimacs");
            //        bool startFlag = true;
            //        int id = 0;
            //        string data = "";
            //        foreach (string line in allLines)
            //        {
            //            if (startFlag)
            //            {
            //                if (line.Contains("PROBLEM ID"))
            //                {
            //                    string problemID = line.Replace("PROBLEM ID = ", "");
            //                    id = int.Parse(problemID);
            //                    startFlag = false;
            //                    data = "";
            //                }
            //            }
            //            else
            //            {
            //                if (string.IsNullOrWhiteSpace(line))
            //                {
            //                    Formula formula = new Formula(data);
            //                    formulas.Add(id, formula);
            //                    startFlag = true;
            //                }
            //                data += line + Environment.NewLine;
            //            }
            //        }
            //        Console.WriteLine("Loaded");

            //        //CALCULATE RESULTS BRUTEFORCE
            //        BruteForceSolver solver = new BruteForceSolver();
            //        StreamWriter writer = File.CreateText("C:\\Users\\jan.tkacik\\Documents\\GitHub\\MI-PAA\\Data\\W3SAT\\Ratio_" + ratio + "_VariablesCount_" + variablesCount + ".results");
            //        foreach (KeyValuePair<int, Formula> formula in formulas)
            //        {
            //            Stopwatch stopwatch = new Stopwatch();
            //            stopwatch.Start();
            //            Tuple<int, BitArray> solve = solver.Solve(formula.Value);
            //            stopwatch.Stop();

            //            writer.WriteLine(formula.Key + "," + solve.Item1 + "," + stopwatch.ElapsedMilliseconds);
            //            Console.WriteLine("Solved - " + formula.Key);
            //            Console.WriteLine(solve.Item1);
            //            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            //        }
            //        writer.Close();
            //    }
            //}

            //Formula problem = InstanceGenerator.InstanceGenerator.GenerateInstance(25, 10);

            //BruteForceSolver solver = new BruteForceSolver();
            //GeneticsSolver geneticsSolver = new GeneticsSolver(100,1000, new RouletteWheelSelection(), 0.1,0.75,0.03,false,false);
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //Tuple<int, BitArray> solve = solver.Solve(problem);
            //stopwatch.Stop();
            
            //Console.WriteLine(solve.Item1);
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);

            //Stopwatch stopwatch2 = new Stopwatch();
            //stopwatch2.Start();
            //Tuple<int, BitArray> solve2 = geneticsSolver.Solve(problem);
            //stopwatch2.Stop();

            //Console.WriteLine(solve2.Item1);
            //Console.WriteLine(stopwatch2.ElapsedMilliseconds);
            
            GridOptimizer.GridOptimizer optimizer = new GridOptimizer.GridOptimizer(3, 10);
            AckleyProblem test = new AckleyProblem();
            double[] optimum = optimizer.Optimize(test);

            Console.WriteLine(optimum[0]);
            Console.WriteLine(optimum[1]);
            
            Console.ReadLine();
        }
    }
}
