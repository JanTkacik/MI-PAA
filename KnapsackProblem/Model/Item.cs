namespace KnapsackProblem.Model
{
    public struct Item
    {
        public int Weight { get; private set; }
        public int Cost { get; private set; }

        public Item(int weight, int cost) : this()
        {
            Weight = weight;
            Cost = cost;
        }
    }
}
