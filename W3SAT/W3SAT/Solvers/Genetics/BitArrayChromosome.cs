using System;
using System.Collections;
using AForge;
using AForge.Genetic;

namespace W3SAT.Solvers.Genetics
{
    public sealed class BitArrayChromosome : ChromosomeBase
    {
        private static readonly Random Random = new ThreadSafeRandom();

        public BitArray Value { get; private set; }
        private readonly int _length;

        public BitArrayChromosome(int length)
        {
            _length = length;
            Generate();
        }

        public override void Generate()
        {
            Value = new BitArray(_length);
            for (int i = 0; i < _length; i++)
            {
                Value[i] = Random.NextDouble() < 0.5;
            }
        }

        public override IChromosome CreateNew()
        {
            return new BitArrayChromosome(_length);
        }

        public override IChromosome Clone()
        {
            BitArrayChromosome clone = new BitArrayChromosome(_length)
                {
                    Value = new BitArray(Value),
                    fitness = fitness
                };
            return clone;
        }

        public override void Mutate()
        {
            int index = Random.Next(_length);
            Value[index] = !Value[index];
        }

        public override void Crossover(IChromosome pair)
        {
            BitArrayChromosome p = (BitArrayChromosome) pair;
            if (p != null && p._length == _length)
            {
                int crossoverPoint = Random.Next(_length);
                BitArray mask1 = new BitArray(_length, false);
                BitArray mask2 = new BitArray(_length, true);
                for (int i = crossoverPoint; i < _length; i++)
                {
                    mask1[i] = true;
                    mask2[i] = false;
                }
                BitArray newOne = Value.And(mask1).Or(p.Value.And(mask2));
                BitArray newSecond = p.Value.And(mask1).Or(Value.And(mask2));

                Value = newOne;
                p.Value = newSecond;
            }
        }
    }
}
