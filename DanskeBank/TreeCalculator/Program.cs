using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeCalculator.Interfaces;

namespace TreeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length!=1)
                {
                    throw new Exception("Expected exactly one argument which should be the path to the tree input file.");
                }
                var inputFile = args[0];
                if (!System.IO.File.Exists(inputFile))
                {
                    throw new Exception("Failed to find input file: '" + inputFile + "'.");
                }

                var tree = new Data.TreeStructure();
                tree.ReadTree(inputFile);


                // One may specify the class name and load a relevant calculator either through reflection or some class factory. 
                // Here, I just create one on the fly.
                ICalculator calculator = new Calculator.ApplicantChallengeCalculator();
                var result = calculator.Calculator(tree);
                if (result.Any())
                {
                    foreach (var resultValue in result.Values)
                    {
                        System.Console.WriteLine("{0}: {1}", resultValue.Name, resultValue.Result);
                    }
                }
                else
                {
                    System.Console.WriteLine("Calculation completed, but didn't return any results.");
                }


            }
            catch(Exception exc)
            {
                System.Console.WriteLine("An error occurred: " + exc.Message);
                System.Console.WriteLine("");
                System.Console.WriteLine("Full error info: " + exc.ToString());

            }
            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
        }
    }
}
