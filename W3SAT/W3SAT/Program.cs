using System;
using System.Collections;
using System.Diagnostics;
using AForge.Genetic;
using W3SAT.Model;
using W3SAT.Solvers;

namespace W3SAT
{
    class Program
    {
        static void Main(string[] args)
        {
            Formula problem = InstanceGenerator.InstanceGenerator.GenerateInstance(20, 10);
            BruteForceSolver solver = new BruteForceSolver();
            GeneticsSolver geneticsSolver = new GeneticsSolver(100,1000, new RouletteWheelSelection(), 0.1,0.75,0.03,false,false);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Tuple<int, BitArray> solve = solver.Solve(problem);
            stopwatch.Stop();
            
            Console.WriteLine(solve.Item1);
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            Tuple<int, BitArray> solve2 = geneticsSolver.Solve(problem);
            stopwatch2.Stop();

            Console.WriteLine(solve2.Item1);
            Console.WriteLine(stopwatch2.ElapsedMilliseconds);
            

            Console.ReadLine();
        }
    }
}
