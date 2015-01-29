using System;
using System.Collections;

namespace W3SAT.Model
{
    public class Clausule
    {
        private readonly WeightedVariable _variable1;
        private readonly WeightedVariable _variable2;
        private readonly WeightedVariable _variable3;
        private readonly bool _modifier1;
        private readonly bool _modifier2;
        private readonly bool _modifier3;

        public Clausule(bool modifier1, WeightedVariable variable1, bool modifier2, WeightedVariable variable2, bool modifier3, WeightedVariable variable3)
        {
            _modifier1 = modifier1;
            _variable1 = variable1;
            _modifier2 = modifier2;
            _variable2 = variable2;
            _modifier3 = modifier3;
            _variable3 = variable3;
        }

        public Clausule(string dimacsValue)
        {
            string[] values = dimacsValue.Split(' ');
            int weight1 = int.Parse(values[0]);
            int id1 = int.Parse(values[1]);
            int weight2 = int.Parse(values[2]);
            int id2 = int.Parse(values[3]);
            int weight3 = int.Parse(values[4]);
            int id3 = int.Parse(values[5]);

            _variable1 = new WeightedVariable(Math.Abs(id1), weight1);
            _variable2 = new WeightedVariable(Math.Abs(id2), weight2);
            _variable3 = new WeightedVariable(Math.Abs(id3), weight3);

            _modifier1 = !values[1].Contains("-");
            _modifier1 = !values[3].Contains("-");
            _modifier1 = !values[5].Contains("-");
        }

        public bool IsSatisfied(bool value1, bool value2, bool value3)
        {
            return (_modifier1 == value1) || (_modifier2 == value2) || (_modifier3 == value3);
        }

        public int GetWeight(bool value1, bool value2, bool value3)
        {
            int weight = 0;
            if (value1)
            {
                weight += _variable1.Weight;
            }
            if (value2)
            {
                weight += _variable2.Weight;
            }
            if (value3)
            {
                weight += _variable3.Weight;
            }
            return weight;
        }

        public bool IsSatisfied(BitArray variableValues)
        {
            return IsSatisfied(variableValues[_variable1.Id], variableValues[_variable2.Id], variableValues[_variable3.Id]);
        }

        public int GetWeight(BitArray variableValues)
        {
            return GetWeight(variableValues[_variable1.Id], variableValues[_variable2.Id], variableValues[_variable3.Id]);
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", _variable1.ToString(_modifier1), _variable2.ToString(_modifier2), _variable3.ToString(_modifier3));
        }

        public bool IsUsingVariable(int index)
        {
            return _variable1.Id == index || _variable2.Id == index || _variable3.Id == index;
        }

        public int GetVariableWeight(int index)
        {
            if (_variable1.Id == index)
            {
                return _variable1.Weight;
            }
            if (_variable2.Id == index)
            {
                return _variable2.Weight;
            }
            if (_variable3.Id == index)
            {
                return _variable3.Weight;
            }
            return 0;
        }
    }
}
