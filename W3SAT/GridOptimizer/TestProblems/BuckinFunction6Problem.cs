using System;

namespace GridOptimizer.TestProblems
{
    public class BuckinFunction6Problem : IOptimizationProblem
    {
        public int DimensionsCount { get { return 2; } }
        public double GetMinForDimension(int dimension)
        {
            return -20;
        }

        public double GetMaxForDimension(int dimension)
        {
            return 20;
        }

        public double GetStepForDimension(int dimension)
        {
            return 0.13;
        }

        public double GetStepChangeForDimension(int dimension)
        {
            return 0.5;
        }

        public double Evaluate(Configuration configuration)
        {
            double x = configuration.GetValueForDimension(0);
            double y = configuration.GetValueForDimension(1);
            return 100*Math.Sqrt(Math.Abs(y - 0.01*x*x)) + 0.01*Math.Abs(x + 10);
        }
    }
}
