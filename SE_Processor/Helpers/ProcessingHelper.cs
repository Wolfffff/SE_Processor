using System;
namespace SE_Processor.Helpers
{
    //If we want to optimize it, we can put all of this into a single function but because the low complexity, there's no reason to worry about processing speedcat 
    public class ProcessingHelper
    {
        public static double SuccessRate(string[,] array){
            double failed = 0;
            var length = array.GetLength(0);

            for (int i = 0; i < length; i++)
            {
                if (array[i,1] != "Valid")
                {
                    failed += 1;
                }
            }
            return (length - failed)/length;
        }

        public static double SEA1(string[,] array)
        {
            double failed = 0;
            double partitionCount = 1;

            var length = array.GetLength(0);
            var tracker = Convert.ToDateTime(array[0, 0]);
            var startTime = new DateTime(tracker.Year, tracker.Month, tracker.Day, 0, 0, 0, 0);
            var tick = 0;
            for (int i = 0; i < length; i++)
            {
                if((Convert.ToDateTime(array[i,0]) - startTime).TotalMinutes > 10){
                    partitionCount += 1;
                    if (tick != 1)
                    {
                        failed += 1;
                    }
                    tick = 0;
                    startTime = startTime.AddMinutes(10);
                }
                if (array[i, 1] == "Valid"){
                    tick = 1;
                }


            }
            return (partitionCount - failed) / partitionCount;
        }

        public static Tuple<int,string> GapCountAndMaxSize(string[,] array){
            var gapCount = 0;
            double maxSize = 0;
            var startTime = Convert.ToDateTime(array[0, 0]);
            var started = false;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i, 1] != "Valid")
                {
                    if(!started){
                        started = true;
                        //These if cover if the first entry is not valid
                        if(array[i-1,0]!= null){ 
                            startTime = Convert.ToDateTime(array[i - 1, 0]); 
                        }
                        else if (array[i - 1, 0] == null){
                            startTime = Convert.ToDateTime(array[i, 0]);
                        }

                    }


                }else if(array[i,1] == "Valid"){
                    if(started){
                        started = false;
                        if((Convert.ToDateTime(array[i, 0]) - startTime).TotalMinutes > 30){
                gapCount += 1;
                        }
                        if (maxSize < (Convert.ToDateTime(array[i,0]) - startTime).TotalSeconds)
                        {
                            maxSize = (Convert.ToDateTime(array[i, 0]) - startTime).TotalSeconds;
                        }
                    }
                }
            }
            return new Tuple<int, string>(gapCount, string.Format("{0:00}:{1:00}:{2:00}", maxSize / 3600, (maxSize / 60), maxSize % 60));
        }
    }
}
