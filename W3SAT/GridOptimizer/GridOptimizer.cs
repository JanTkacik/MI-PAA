using System;
using System.Collections.Generic;

namespace GridOptimizer
{
    //TEST http://en.wikipedia.org/wiki/Test_functions_for_optimization
    //TODO implement other optimalizators
    public class GridOptimizer
    {
        private readonly int _searchDepth;
        private readonly int _spaceCoverageIndex;

        public GridOptimizer(int spaceCoverageIndex, int searchDepth)
        {
            _spaceCoverageIndex = spaceCoverageIndex;
            _searchDepth = searchDepth;
        }

        public double[] Optimize(IOptimizationProblem problem)
        {
            Tuple<double,Configuration> configuration = Optimize(problem, 0);
            return configuration.Item2.Value;
        }

        private Tuple<double,Configuration> Optimize(IOptimizationProblem problem, int depth)
        {
            if (depth > _searchDepth)
            {
                return null;
            }

            List<Configuration> firstSamples = GenerateFirstSamples(problem);
            List<Tuple<double,Configuration>> evaluations = new List<Tuple<double, Configuration>>(firstSamples.Count);

            foreach (Configuration sample in firstSamples)
            {
                evaluations.Add(new Tuple<double, Configuration>(problem.Evaluate(sample),sample));
            }

            evaluations.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            List<IOptimizationProblem> newProblems = new List<IOptimizationProblem>(_spaceCoverageIndex);
            for (int i = 0; i < _spaceCoverageIndex; i++)
            {
                if (i < evaluations.Count)
                {
                    newProblems.Add(new SubOptimizationProblem(evaluations[i].Item2, problem));
                }
            }

            Tuple<double, Configuration> best = evaluations[0];

            foreach (IOptimizationProblem newProblem in newProblems)
            {
                Tuple<double, Configuration> optimal = Optimize(newProblem, depth + 1);
                if (optimal != null)
                {
                    if (optimal.Item1 < best.Item1)
                    {
                        best = optimal;
                    }
                }
            }

            return best;
        }

        private List<Configuration> GenerateFirstSamples(IOptimizationProblem problem)
        {
            List<Configuration> returnList = new List<Configuration>();
            int dimensionsCount = problem.DimensionsCount;
            returnList.Add(new Configuration(dimensionsCount));

            for (int i = 0; i < dimensionsCount; i++)
            {
                double min = problem.GetMinForDimension(i);
                double max = problem.GetMaxForDimension(i);
                double step = problem.GetStepForDimension(i);

                List<Configuration> newConfigurations = new List<Configuration>();

                foreach (Configuration conf in returnList)
                {
                    for (double j = min; j < max; j += step)
                    {
                        Configuration configuration = conf.Clone();
                        configuration.SetValueForDimension(i, j);
                        newConfigurations.Add(configuration);
                    }

                    conf.SetValueForDimension(i, max);
                }

                returnList.AddRange(newConfigurations);
            }

            return returnList;
        }
    }

    public interface IOptimizationProblem
    {
        int DimensionsCount { get; }
        double GetMinForDimension(int dimension);
        double GetMaxForDimension(int dimension);
        double GetStepForDimension(int dimension);
        double GetStepChangeForDimension(int dimension);
        double Evaluate(Configuration configuration);
    }

    internal class SubOptimizationProblem : IOptimizationProblem
    {
        private readonly Configuration _sample;
        private readonly IOptimizationProblem _parentProblem;

        public SubOptimizationProblem(Configuration sample, IOptimizationProblem parentProblem)
        {
            _sample = sample;
            _parentProblem = parentProblem;
        }

        public int DimensionsCount { get { return _parentProblem.DimensionsCount; } }

        public double GetMinForDimension(int dimension)
        {
            double center = _sample.GetValueForDimension(dimension);
            double min = center - (_parentProblem.GetStepForDimension(dimension) / 2);
            if (min < _parentProblem.GetMinForDimension(dimension))
            {
                min = _parentProblem.GetMinForDimension(dimension);
            }
            return min;
        }

        public double GetMaxForDimension(int dimension)
        {
            double center = _sample.GetValueForDimension(dimension);
            double max = center + (_parentProblem.GetStepForDimension(dimension) / 2);
            if (max > _parentProblem.GetMaxForDimension(dimension))
            {
                max = _parentProblem.GetMaxForDimension(dimension);
            }
            return max;
        }

        public double GetStepForDimension(int dimension)
        {
            return _parentProblem.GetStepForDimension(dimension) * GetStepChangeForDimension(dimension);
        }

        public double GetStepChangeForDimension(int dimension)
        {
            return _parentProblem.GetStepChangeForDimension(dimension);
        }

        public double Evaluate(Configuration configuration)
        {
            return _parentProblem.Evaluate(configuration);
        }
    }

    public class Configuration
    {
        public int DimensionsCount { get; private set; }

        public double[] Value
        {
            get { return _value; }
        }

        private readonly double[] _value;
        private readonly bool[] _setDimensions;

        public Configuration(int dimensionsCount)
        {
            DimensionsCount = dimensionsCount;
            _value = new double[dimensionsCount];
            _setDimensions = new bool[dimensionsCount];
        }

        public Configuration(double[] value) : this(value.Length)
        {
            Array.Copy(value, _value, DimensionsCount);
            _setDimensions = new bool[DimensionsCount];
            for (int i = 0; i < DimensionsCount; i++)
            {
                _setDimensions[i] = true;
            }
        }

        private Configuration(int dimensionsCount, double[] value, bool[] setDimensions)
            : this(dimensionsCount)
        {
            Array.Copy(value, _value, dimensionsCount);
            Array.Copy(setDimensions, _setDimensions, dimensionsCount);
        }

        

        public bool IsDimensionSet(int dimension)
        {
            return _setDimensions[dimension];
        }

        public void SetValueForDimension(int dimension, double value)
        {
            if (_setDimensions[dimension])
            {
                throw new ArgumentException("Dimension can be set only once", "dimension");
            }

            _value[dimension] = value;
            _setDimensions[dimension] = true;
        }

        public double GetValueForDimension(int dimension)
        {
            return _value[dimension];
        }

        public Configuration Clone()
        {
            return new Configuration(DimensionsCount, _value, _setDimensions);
        }
    }
}
