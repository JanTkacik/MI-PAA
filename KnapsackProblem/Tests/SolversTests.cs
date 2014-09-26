using System.Collections.Generic;
using KnapsackProblem.Algorithms;
using KnapsackProblem.Model;
using NUnit.Framework;

namespace KnapsackProblem.Tests
{
    [TestFixture]
    class SolversTests
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

        

        [Test]
        public void Solve_ShouldReturnCorrectValue_IfAllItemsShouldBeInBag()
        {
            KnapsackProblemModel problem = new KnapsackProblemModel(9005, 100, new List<Item>
                {
                    new Item(12, 66, 0), new Item(52, 167, 1), new Item(14, 150, 2), new Item(3, 180, 3)
                });

            IKnapsackSolver solver = new RatioHeuristicSolver();
            int solve = solver.Solve(problem);

            Assert.AreEqual(563, solve);

            IKnapsackSolver solver2 = new BruteForceSolver();
            int solve2 = solver.Solve(problem);

            Assert.AreEqual(563, solve2);
        }
    }
}
