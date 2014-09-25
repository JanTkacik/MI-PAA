using System.Collections.Generic;
using KnapsackProblem.Algorithms;
using KnapsackProblem.Model;
using NUnit.Framework;

namespace KnapsackProblem.Tests
{
    [TestFixture]
    class BruteForceSolverTest
    {
        [Test]
        public void SimpleTest()
        {
            KnapsackProblemModel problem = new KnapsackProblemModel(9000, 100, new List<Item>
                {
                    new Item(18, 114, 0), new Item(42, 136, 1), new Item(88, 192, 2), new Item(3, 223, 3)
                });

            IKnapsackSolver solver = new BruteForceSolver();
            int solve = solver.Solve(problem);

            Assert.AreEqual(473, solve);
        }
    }
}
