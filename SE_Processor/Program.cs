using System;
using SE_Processor.Helpers;

namespace SE_Processor
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Entergy State Estimator CSV Processor \n \n");
            Console.Write("Please type exact directory of CSV files. \nNote: folder should container only relevant CSV files with proper names.\n");
            var location = Console.ReadLine();
            var array = CSVHelper.CombineAndConvert(location);

            var successRate = ProcessingHelper.SuccessRate(array);
            Console.Write("\nSuccess rate is " + successRate * 100 + "%.\n\n");

            var sea1 = ProcessingHelper.SEA1(array);
            Console.Write("SEA1 is " + sea1 * 100 + "%.\n\n");

            var largeGapCountAndMaxGap = ProcessingHelper.GapCountAndMaxSize(array);
            Console.Write("Number of gaps with time(in minutes) greater than 30 is " + largeGapCountAndMaxGap.Item1 + ".\n\n");
            Console.Write("Greatest gap is " + largeGapCountAndMaxGap.Item2 + " (HH:MM:SS).\n");

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
