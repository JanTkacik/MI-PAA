using System;
using System.Collections;
using System.Diagnostics;
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Tuple<int, BitArray> solve = solver.Solve(problem);
            stopwatch.Stop();
            
            Console.WriteLine(solve.Item1);
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            

            Console.ReadLine();
        }
    }
}
