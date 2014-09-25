using CommandLine;
using CommandLine.Text;

namespace KnapsackProblem.Configuration
{
    class Options
    {
        [OptionArray('p', "problems", DefaultValue = null, HelpText = "Files with problems to be solved", Required = true)]
        public string[] InputFiles { get; set; }

        [OptionArray('r', "results", DefaultValue = null, HelpText = "Correct results for error calculation", Required = false)]
        public string[] ResultFiles { get; set; }

        [Option('v', "verbose", HelpText = "Print details during execution.", DefaultValue = false, Required = false)]
        public bool Verbose { get; set; }

        [Option('o', "output", HelpText = "Path to output files - if not specified, results will be printed to console", DefaultValue = null, Required = false)]
        public string OutputFilePath { get; set; }
        
        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("<<Knapsack problem solver>>", "<<1.0>>"),
                Copyright = new CopyrightInfo("<<Jan Tkacik>>", 2014),
                AddDashesToOption = true
            };
            help.AddPreOptionsLine("Usage: -p problem.dat problem2.dat -r result.dat result2.dat");
            help.AddOptions(this);
            return help;
        }
    }
}
