using System;
using System.Collections;
using W3SAT.Model;
using W3SAT.Solvers;

namespace W3SAT
{
    class Program
    {
        static void Main(string[] args)
        {
            Formula problem = InstanceGenerator.InstanceGenerator.GenerateInstance(10, 10);
            BruteForceSolver solver = new BruteForceSolver();
            Tuple<int, BitArray> solve = solver.Solve(problem);

            Console.WriteLine(solve);
        }
    }
}
