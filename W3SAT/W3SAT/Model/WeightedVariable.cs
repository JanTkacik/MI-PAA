namespace W3SAT.Model
{
    public class WeightedVariable
    {
        private readonly int _id;
        private readonly int _weight;

        public int Id
        {
            get { return _id; }
        }

        public int Weight
        {
            get { return _weight; }
        }

        public WeightedVariable(int id, int weight)
        {
            _id = id;
            _weight = weight;
        }

        public string ToString(bool modifier)
        {
            return string.Format("{0}{1}{2}", _weight, modifier ? " " : " -", _id);
        }

        public override string ToString()
        {
            return string.Format("Id: {0}, Weight: {1}", Id, Weight);
        }
    }
}
