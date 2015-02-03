using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AForge.Genetic;
using W3SAT.Model;
using W3SAT.Solvers;
using W3SAT.Solvers.Genetics;
using RouletteWheelSelection = AForge.Genetic.RouletteWheelSelection;

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

            //LOAD DATA 
            for (int variablesCount = 100; variablesCount <= 100; variablesCount += 10)
            {
                for (int i = 4; i < 10; i++)
                {
                    double ratio = 2 + (i * 0.5);

                    Dictionary<int, Formula> formulas = new Dictionary<int, Formula>();
                    string[] allLines = File.ReadAllLines("C:\\Users\\jantk_000\\Documents\\GitHub\\MI-PAA\\Data\\W3SAT\\Ratio_" + ratio + "_VariablesCount_" + variablesCount + ".dimacs");
                    bool startFlag = true;
                    int id = 0;
                    string data = "";
                    foreach (string line in allLines)
                    {
                        if (startFlag)
                        {
                            if (line.Contains("PROBLEM ID"))
                            {
                                string problemID = line.Replace("PROBLEM ID = ", "");
                                id = int.Parse(problemID);
                                startFlag = false;
                                data = "";
                            }
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(line))
                            {
                                Formula formula = new Formula(data);
                                formulas.Add(id, formula);
                                startFlag = true;
                            }
                            data += line + Environment.NewLine;
                        }
                    }
                    Console.WriteLine("Loaded -varCnt: " + variablesCount + " -ratio: " + ratio);

                    //GridOptimizer.GridOptimizer optimizer = new GridOptimizer.GridOptimizer(3, 1);
                    //GeneticsMetaOptimization metaOptimization = new GeneticsMetaOptimization(new List<Formula> { formulas[0] });
                    //double[] optimum = optimizer.Optimize(metaOptimization);

                    //Console.WriteLine(optimum[0]);
                    //Console.WriteLine(optimum[1]);
                    //Console.WriteLine(optimum[2]);
                    //Console.ReadLine();


                    //GeneticsSolver solver = new GeneticsSolver(30, 100, new RouletteWheelSelection(), 1, 0.13, 0.13, true, true);
                    //StreamWriter writer = File.CreateText("C:\\Users\\jantk_000\\Documents\\GitHub\\MI-PAA\\Data\\W3SAT\\Ratio_" + ratio + "_VariablesCount_" + variablesCount + ".results.rws");
                    //foreach (KeyValuePair<int, Formula> formula in formulas)
                    //{
                    //    Stopwatch stopwatch = new Stopwatch();
                    //    stopwatch.Start();
                    //    Tuple<int, BitArray> solve = solver.Solve(formula.Value);
                    //    stopwatch.Stop();

                    //    writer.WriteLine(formula.Key + "," + solve.Item1 + "," + stopwatch.ElapsedMilliseconds);
                    //    Console.WriteLine("Solved - " + formula.Key);
                    //    Console.WriteLine(solve.Item1);
                    //    Console.WriteLine(stopwatch.ElapsedMilliseconds);
                    //}
                    //writer.Close();

                    GeneticsSolver solver = new GeneticsSolver(30, 1000, new EliteSelection(), 0.98, 0.7, 0.08, true, true);
                    StreamWriter writer = File.CreateText("C:\\Users\\jantk_000\\Documents\\GitHub\\MI-PAA\\Data\\W3SAT\\Ratio_" + ratio + "_VariablesCount_" + variablesCount + ".results.elt");
                    foreach (KeyValuePair<int, Formula> formula in formulas)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        Tuple<int, BitArray> solve = solver.Solve(formula.Value);
                        stopwatch.Stop();

                        writer.WriteLine(formula.Key + "," + solve.Item1 + "," + stopwatch.ElapsedMilliseconds);
                        Console.WriteLine("Solved - " + formula.Key);
                        Console.WriteLine(solve.Item1);
                        Console.WriteLine(stopwatch.ElapsedMilliseconds);
                    }
                    writer.Close();

                    //solver = new GeneticsSolver(30, 100, new RankSelection(), 0.75, 0.18, 0.05, true, true);
                    //writer = File.CreateText("C:\\Users\\jantk_000\\Documents\\GitHub\\MI-PAA\\Data\\W3SAT\\Ratio_" + ratio + "_VariablesCount_" + variablesCount + ".results.rnk");
                    //foreach (KeyValuePair<int, Formula> formula in formulas)
                    //{
                    //    Stopwatch stopwatch = new Stopwatch();
                    //    stopwatch.Start();
                    //    Tuple<int, BitArray> solve = solver.Solve(formula.Value);
                    //    stopwatch.Stop();

                    //    writer.WriteLine(formula.Key + "," + solve.Item1 + "," + stopwatch.ElapsedMilliseconds);
                    //    Console.WriteLine("Solved - " + formula.Key);
                    //    Console.WriteLine(solve.Item1);
                    //    Console.WriteLine(stopwatch.ElapsedMilliseconds);
                    //}
                    //writer.Close();
                }
            }

            //Summarize();

            Console.ReadLine();
        }

        private static void Summarize()
        {
            StreamWriter writer = File.CreateText("C:\\Users\\jantk_000\\Documents\\GitHub\\MI-PAA\\Data\\W3SAT\\Summary.results");
            writer.WriteLine("VCnt,Rat,ID,BFR,BFT,RWR,RWT,ELR,ELT,RNR,RNT");
            
            for (int variablesCount = 10; variablesCount <= 100; variablesCount += 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    double ratio = 2 + (i * 0.5);

                    Console.WriteLine(variablesCount + " " + ratio);

                    Dictionary<int, int> BFR = new Dictionary<int, int>();
                    Dictionary<int, int> BFT = new Dictionary<int, int>();
                    Dictionary<int, int> RWR = new Dictionary<int, int>();
                    Dictionary<int, int> RWT = new Dictionary<int, int>();
                    Dictionary<int, int> ELR = new Dictionary<int, int>();
                    Dictionary<int, int> ELT = new Dictionary<int, int>();
                    Dictionary<int, int> RNR = new Dictionary<int, int>();
                    Dictionary<int, int> RNT = new Dictionary<int, int>();

                    FillDictionary(ratio, variablesCount, BFR, BFT, "");
                    FillDictionary(ratio, variablesCount, RWR, RWT, ".rws");
                    FillDictionary(ratio, variablesCount, ELR, ELT, ".elt");
                    FillDictionary(ratio, variablesCount, RNR, RNT, ".rnk");
                    
                    for (int j = 0; j < 100; j++)
                    {
                        writer.Write(variablesCount + "," + ratio + "," + j);
                        WriteData(writer, BFR, j, BFT);
                        WriteData(writer, RWR, j, RWT);
                        WriteData(writer, ELR, j, ELT);
                        WriteData(writer, RNR, j, RNT);
                        writer.WriteLine();
                    }
                }
            }

            writer.Close();
        }

        private static void WriteData(StreamWriter writer, Dictionary<int, int> BFR, int j, Dictionary<int, int> BFT)
        {
            writer.Write(",");
            if (BFR.ContainsKey(j))
            {
                writer.Write(BFR[j] + "," + BFT[j]);
            }
            else
            {
                writer.Write("N/A,N/A");
            }
        }

        private static void FillDictionary(double ratio, int variablesCount, Dictionary<int, int> res, Dictionary<int, int> time, string postfix)
        {
            string path = "C:\\Users\\jantk_000\\Documents\\GitHub\\MI-PAA\\Data\\W3SAT\\Ratio_" + ratio + "_VariablesCount_" +
                          variablesCount + ".results" + postfix;
            if (File.Exists(path))
            {
                string[] readAllLines = File.ReadAllLines(path);
                foreach (string line in readAllLines)
                {
                    string[] data = line.Split(',');
                    res.Add(int.Parse(data[0]), int.Parse(data[1]));
                    time.Add(int.Parse(data[0]), int.Parse(data[2]));
                }
            }
        }
    }
}
