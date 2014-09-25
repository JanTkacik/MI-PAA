using System.Collections.Generic;
using System.IO;
using KnapsackProblem.Model;

namespace KnapsackProblem.Helpers
{
    class DataParser
    {
        public IEnumerable<KnapsackProblemModel> ParseProblem(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return ParseProblem(reader);
            }
        }

        public IEnumerable<KnapsackProblemModel> ParseProblem(TextReader reader)
        {
            List<KnapsackProblemModel> models = new List<KnapsackProblemModel>();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                models.Add(ParseProblemLine(line));
            }

            return models;
        }

        private KnapsackProblemModel ParseProblemLine(string line)
        {
            string[] data = line.Split(' ');
            int id = int.Parse(data[0]);
            int count = int.Parse(data[1]);
            int capacity = int.Parse(data[2]);
            List<Item> items = new List<Item>(count);
            for (int i = 0; i < (count*2); i = i + 2)
            {
                items.Add(new Item(int.Parse(data[i + 3]), int.Parse(data[i + 4]), i/2));
            }

            return new KnapsackProblemModel(id, capacity, items);
        }
    }
}
