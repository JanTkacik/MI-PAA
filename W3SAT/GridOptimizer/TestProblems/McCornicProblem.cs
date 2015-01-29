using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridOptimizer.TestProblems
{
    public class McCornicProblem : IOptimizationProblem
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
            return 0.1;
        }

        public double GetStepChangeForDimension(int dimension)
        {
            return 0.1;
        }

        public double Evaluate(Configuration configuration)
        {
            double x = configuration.GetValueForDimension(0);
            double y = configuration.GetValueForDimension(1);
            return Math.Sin(x + y) + Math.Pow(x - y, 2) - 1.5*x + 2.5*y + 1;
        }
    }
}
