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

            IKnapsackSolver solver2 = new BranchAndBoundSolver();
            int solve2 = solver2.Solve(problem);

            Assert.AreEqual(473, solve2);

            IKnapsackSolver solver3 = new DynamicByCost();
            int solve3 = solver3.Solve(problem);

            Assert.AreEqual(473, solve3);
        }

        [Test]
        public void SimpleTest3()
        {
            KnapsackProblemModel problem = new KnapsackProblemModel(9013, 100, new List<Item>
                {
                    new Item(11, 166, 0), new Item(24, 165, 1), new Item(50, 74, 2), new Item(21, 100, 3)
                });

            IKnapsackSolver solver = new BruteForceSolver();
            int solve = solver.Solve(problem);

            Assert.AreEqual(431, solve);

            IKnapsackSolver solver2 = new BranchAndBoundSolver();
            int solve2 = solver2.Solve(problem);

            Assert.AreEqual(431, solve2);

            IKnapsackSolver solver3 = new DynamicByCost();
            int solve3 = solver3.Solve(problem);

            Assert.AreEqual(431, solve3);
        }

        [Test]
        public void SimpleTest4()
        {
            KnapsackProblemModel problem = new KnapsackProblemModel(9012, 100, new List<Item>
                {
                    new Item(51, 87, 0), new Item(5, 198, 1), new Item(19, 40, 2), new Item(81, 47, 3)
                });

            IKnapsackSolver solver = new BruteForceSolver();
            int solve = solver.Solve(problem);

            Assert.AreEqual(325, solve);

            IKnapsackSolver solver2 = new BranchAndBoundSolver();
            int solve2 = solver2.Solve(problem);

            Assert.AreEqual(325, solve2);

            IKnapsackSolver solver3 = new DynamicByCost();
            int solve3 = solver3.Solve(problem);

            Assert.AreEqual(325, solve3);
        }

        [Test]
        public void SimpleTest2()
        {
            KnapsackProblemModel problem = new KnapsackProblemModel(9000, 100, new List<Item>
                {
                    new Item(89, 196, 0), new Item(18, 62, 1), new Item(57, 34, 2), new Item(69, 112, 3)
                });

            IKnapsackSolver solver = new RatioHeuristicSolver();
            int solve = solver.Solve(problem);

            //Assert.AreEqual(473, solve);
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
            int solve2 = solver2.Solve(problem);

            Assert.AreEqual(563, solve2);

            IKnapsackSolver solver3 = new BranchAndBoundSolver();
            int solve3 = solver3.Solve(problem);

            Assert.AreEqual(563, solve3);

            IKnapsackSolver solver4 = new DynamicByCost();
            int solve4 = solver4.Solve(problem);

            Assert.AreEqual(563, solve4);
        }
    }
}
