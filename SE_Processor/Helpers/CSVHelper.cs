using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SE_Processor.Helpers
{
    public class CSVHelper
    {
        
        public static string[,] CombineAndConvert(string location){
            try
            {
                // Specify wildcard search to match CSV files that will be combined - we may want to use a more specfic wildcard depending on the contents of the folder.
                string[] filePaths = Directory.GetFiles(location, "*.csv");
                //Soft from oldest to newest
                Array.Sort(filePaths, StringComparer.InvariantCulture);
                filePaths.Reverse();
                var outputLines = new List<string[]>();

                for (int i = 0; i < filePaths.Length; i++)
                {
                    string file = filePaths[i];

                    //Skip header
                    List<string> lines = File.ReadAllLines(file).Skip(1).ToList();


                    foreach (string line in lines)
                    {
                        //Only take date and status instead of whole thing
                        outputLines.Add(line.Split(",").Take(2).ToArray());
                    }
                }

                var R = outputLines.Count;
                var C = outputLines[0].Length;
                var arr = new string[R, C];
                for (int r = 0; r != R; r++)
                {
                    for (int c = 0; c != C; c++)
                    {
                        arr[r, c] = outputLines[r][c];
                    }
                }
                return arr;
            }catch(Exception e){
                throw new Exception("Error in finding or preprocessing \n",e);
            }
        
        }
    }
}
