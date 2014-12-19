using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace W3SAT.Model
{
    public class Formula
    {
        public int VariablesCount { get { return _variablesCount; } }

        private readonly int _variablesCount;
        private readonly List<Clausule> _clausules;

        public Formula(IEnumerable<Clausule> clausules, int variablesCount)
        {
            _variablesCount = variablesCount;
            _clausules = new List<Clausule>(clausules);
        }

        public Formula(string dimacsValue)
        {
            string[] lines = dimacsValue.Split(new[]{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (line.StartsWith("c"))
                {
                    continue;
                }
                if (line.StartsWith("p"))
                {
                    string[] data = line.Split(' ');
                    _variablesCount = int.Parse(data[2]);
                    continue;
                }
                _clausules.Add(new Clausule(line));
            }
        }

        public bool IsSatisfied(BitArray variableValues)
        {
            foreach (var clausule in _clausules)
            {
                if (!clausule.IsSatisfied(variableValues))
                {
                    return false;
                }
            }
            return true;
        }

        public int GetWeight(BitArray variableValues)
        {
            int weight = 0;
            foreach (var clausule in _clausules)
            {
                weight += clausule.GetWeight(variableValues);
            }
            return weight;
        }

        public bool IsSatisfiedSafe(BitArray variablesValues)
        {
            CheckVariableCount(variablesValues);
            return IsSatisfied(variablesValues);
        }
        
        public int GetWeightSafe(BitArray variableValues)
        {
            CheckVariableCount(variableValues);
            return GetWeight(variableValues);
        }

        private void CheckVariableCount(BitArray variablesValues)
        {
            if (variablesValues.Length != _variablesCount)
            {
                throw new ArgumentException("Invalid argument - bit array must contain " + _variablesCount + " values",
                                            "variablesValues");
            }
        }

        public override string ToString()
        {
            StringBuilder dimacs = new StringBuilder();
            dimacs.AppendFormat("p WCNF {0} {1}", _variablesCount, _clausules.Count);
            dimacs.AppendLine();
            foreach (Clausule clausule in _clausules)
            {
                dimacs.AppendLine(clausule.ToString());
            }
            return dimacs.ToString();
        }
    }
}
