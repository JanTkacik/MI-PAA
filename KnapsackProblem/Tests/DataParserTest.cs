using System.Collections.Generic;
using System.IO;
using System.Linq;
using KnapsackProblem.Helpers;
using KnapsackProblem.Model;
using NUnit.Framework;

namespace KnapsackProblem.Tests
{
    [TestFixture]
    class DataParserTest
    {
        [Test]
        public void ParseProblem_ShouldReturnCorrectResult_IfCorrectDataArePassed()
        {
            const string data = "9000 4 100 18 114 42 136 88 192 3 223";
            StringReader stringReader = new StringReader(data);
            
            DataParser parser = new DataParser();
            IEnumerable<KnapsackProblemModel> knapsackProblemModels = parser.ParseProblem(stringReader);

            List<KnapsackProblemModel> problemModels = knapsackProblemModels.ToList();
            KnapsackProblemModel parsedModel = problemModels[0];

            KnapsackProblemModel refereceModel = new KnapsackProblemModel(9000, 100, new List<Item>
                {
                    new Item(18, 114, 0), new Item(42, 136, 1), new Item(88, 192, 2), new Item(3, 223, 3)
                });

            Assert.AreEqual(refereceModel, parsedModel);
        }

        [Test]
        public void ParseResult_ShouldReturnCorrectResult_IfCorrectDataArePassed()
        {
            const string data = "9000 4 473  1 1 0 1";
            StringReader stringReader = new StringReader(data);

            DataParser parser = new DataParser();
            Dictionary<int, int> dictionary = parser.ParseResults(stringReader);
            
            Assert.AreEqual(473, dictionary[9000]);
        }
    }
}
