using KnapsackProblem.Model;

namespace KnapsackProblem.Algorithms
{
    interface IKnapsackSolver
    {
        KnapsackResultModel Solve(KnapsackProblemModel problem);
    }
}
