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

        [Option('a', "bruteforce", HelpText = "Runs brute force algorithm", DefaultValue = false, Required = false)]
        public bool BruteForce { get; set; }

        [Option('b', "ratioheuristic", HelpText = "Runs cost to weight ratio heuristics", DefaultValue = false, Required = false)]
        public bool CostToRatioHeuristics { get; set; }

        [Option('c', "branchandbound", HelpText = "Runs branch and bound optimized brute force algorithm", DefaultValue = false, Required = false)]
        public bool BranchAndBound { get; set; }

        [Option('d', "dynamicbycost", HelpText = "Runs dynamic programming technique with cost decomposition", DefaultValue = false, Required = false)]
        public bool DynamicByCost { get; set; }

        [Option('e', "fptas", HelpText = "Runs dynamic programming technique with FPTAS", DefaultValue = false, Required = false)]
        public bool FPTAS { get; set; }

        [Option('s', "accuracy", HelpText = "FPTAS Accuracy", DefaultValue = 1, Required = false)]
        public int FPTASAccuracy { get; set; }

        [Option('g', "genetics", HelpText = "Run genetics algorithm", DefaultValue = false, Required = false)]
        public bool Genetics { get; set; }

        [Option('i', "iterationscount", HelpText = "Genetic iterations count", DefaultValue = 1000, Required = false)]
        public int IterationsCount { get; set; }

        [Option('m', "mutationrate", HelpText = "Genetic mutation rate", DefaultValue = 0.01, Required = false)]
        public double MutationRate { get; set; }

        [Option('n', "crossoverrate", HelpText = "Genetic crossover rate", DefaultValue = 0.8, Required = false)]
        public double CrossoverRate { get; set; }

        [Option('q', "selectionmethod", HelpText = "Genetic selection method (roulette, rank, elitary)", DefaultValue = "roulette", Required = false)]
        public string SelectionMethod { get; set; }

        [Option('t', "populationsize", HelpText = "Genetic population size", DefaultValue = 30, Required = false)]
        public int PopulationSize { get; set; }

        [Option('w', "randomselectionportion", HelpText = "Genetic random selection portion", DefaultValue = 0, Required = false)]
        public double RandomSelectionPortion { get; set; }

        [Option('z', "geneticsmetaoptimization", HelpText = "Genetic metaoptimization", DefaultValue = false, Required = false)]
        public bool GeneticMetaoptimization { get; set; }

        [Option('y', "geneticsdiversitycheck", HelpText = "Genetic diversity check", DefaultValue = false, Required = false)]
        public bool DiversityCheck { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("<<Knapsack problem solver>>", "<<1.0>>"),
                Copyright = new CopyrightInfo("<<Jan Tkacik>>", 2014),
                AddDashesToOption = true
            };
            help.AddPreOptionsLine("Usage: -p problem.dat problem2.dat -r result.dat result2.dat -a -b -c");
            help.AddOptions(this);
            return help;
        }
    }
}
