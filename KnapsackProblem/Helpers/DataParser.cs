using System;
using System.Collections.Generic;
using System.IO;
using KnapsackProblem.Model;

namespace KnapsackProblem.Helpers
{
    class DataParser
    {
        public IEnumerable<KnapsackProblemModel> ParseProblem(string[] inputFilePaths)
        {
            List<KnapsackProblemModel> models = new List<KnapsackProblemModel>();

            foreach (string inputFilePath in inputFilePaths)
            {
                models.AddRange(ParseProblem(inputFilePath));
            }

            return models;
        }

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


        public Dictionary<int, int> ParseResults(string[] inputFilePaths)
        {
            Dictionary<int, int> results = new Dictionary<int, int>();

            foreach (string inputFilePath in inputFilePaths)
            {
                Dictionary<int, int> tempResults = ParseResults(inputFilePath);
                foreach (KeyValuePair<int, int> result in tempResults)
                {
                    results.Add(result.Key, result.Value);
                }
            }

            return results;
        }

        public Dictionary<int, int> ParseResults(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return ParseResults(reader);
            }
        }

        public Dictionary<int, int> ParseResults(TextReader reader)
        {
            Dictionary<int,int> results = new Dictionary<int, int>();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                KeyValuePair<int, int> result = ParseResultLine(line);
                results.Add(result.Key, result.Value);
            }

            return results;
        }

        private KeyValuePair<int, int> ParseResultLine(string line)
        {
            string[] data = line.Split(' ');
            int id = int.Parse(data[0]);
            int result = int.Parse(data[2]);
            
            return new KeyValuePair<int, int>(id, result);
        }
    }
}
