using System;

namespace GridOptimizer.TestProblems
{
    public class AckleyProblem : IOptimizationProblem
    {
        public int DimensionsCount { get { return 2; } }
        public double GetMinForDimension(int dimension)
        {
            return -5;
        }

        public double GetMaxForDimension(int dimension)
        {
            return 5;
        }

        public double GetStepForDimension(int dimension)
        {
            return 0.27;
        }

        public double GetStepChangeForDimension(int dimension)
        {
            return 0.5;
        }

        public double Evaluate(Configuration configuration)
        {
            double sum1 = 0.0;
            double sum2 = 0.0;

            int dimensionsCount = configuration.DimensionsCount;
            for (int i = 0; i < dimensionsCount; i++)
            {
                double x = configuration.GetValueForDimension(i);
                sum1 += (x * x);
                sum2 += (Math.Cos(2 * Math.PI * x));
            }

            return (-20.0 * Math.Exp(-0.2 * Math.Sqrt(sum1 / dimensionsCount)) -
                            Math.Exp(sum2 / dimensionsCount) + 20.0 + Math.E);

        }
    }
}
