using System.Collections.Generic;
using System.IO;
using KnapsackProblem.Model;

namespace KnapsackProblem.Helpers
{
    class DataParser
    {
        public IEnumerable<KnapsackProblemModel> Parse(string filePath)
        {
            List<KnapsackProblemModel> models = new List<KnapsackProblemModel>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    models.Add(ParseLine(line));
                }
            }

            return models;
        }

        private KnapsackProblemModel ParseLine(string line)
        {
            string[] data = line.Split(' ');
            int id = int.Parse(data[0]);
            int count = int.Parse(data[1]);
            int capacity = int.Parse(data[2]);
            List<Item> items = new List<Item>(count);
            for (int i = 3; i < (count*2) + 3; i = i + 2)
            {
                items.Add(new Item(int.Parse(data[i]), int.Parse(data[i + 1])));
            }

            return new KnapsackProblemModel(id, capacity, items);
        }
    }
}
