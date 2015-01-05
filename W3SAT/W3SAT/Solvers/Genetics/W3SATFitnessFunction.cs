using AForge.Genetic;
using W3SAT.Model;

namespace W3SAT.Solvers.Genetics
{
    public class W3SATFitnessFunction : IFitnessFunction 
    {
        private readonly Formula _problem;

        public W3SATFitnessFunction(Formula problem)
        {
            _problem = problem;
        }

        public double Evaluate(IChromosome chromosome)
        {
            BitArrayChromosome bitArrayChromosome = (BitArrayChromosome) chromosome;

            if(_problem.IsSatisfiedSafe(bitArrayChromosome.Value))
            {
                return _problem.GetWeightSafe(bitArrayChromosome.Value);
            }

            return 0;
        }
    }
}
