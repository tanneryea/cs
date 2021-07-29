using System;
using static System.Console;

namespace CH8HW

{
    class Program
    {
        static void Main(string[] args)

        {
            double[,] waterDepth = new double[6, 5]; //Initialized as 6/5 to have extra space for averages

            string[] location = {"Surf City", "Solomons",

                                 "Hilton Head", "Miami", "Savannah"};

            string[] recordingTime = { "0700 (7am)", "1200 (noon)", "1700 (5pm)", "2100 (9pm)" };

            InitializeArray(waterDepth);

            GetWaterDepths(waterDepth, location, recordingTime);

            FindAvgDepthByLocation(waterDepth);

            FindAvgDepthByTime(waterDepth);

            DisplayTable(waterDepth, location, recordingTime);

            ReadKey();

        }

        public static void InitializeArray(double[,] depth) //Initializes empty array

        {
            Array.Clear(depth, 0, depth.Length);
        }

        public static void GetWaterDepths(double[,] waterDepth, string[] location, string[] recording)

        {

            int locationNum,

                recordingTime;

            string inputValue;

            bool moreData = true;

            while (moreData)

            {

                locationNum = GetLocationNumber(location);

                recordingTime = GetRecTimeNum(recording);

                waterDepth[locationNum, recordingTime] = GetWaterDepths(); 

                Write("\n\nIs there more depths to record? (y/n)");

                inputValue = ReadLine();

                switch (inputValue) //Loop to enter more data if needed

                {

                    case "n":

                    case "N":

                        moreData = false;

                        break;

                    default:

                        moreData = true;

                        break;

                }

            }

        }

        public static int GetLocationNumber(string[] location) //Displays locations and allows selection of one

        {
            int locationNum = -1;
            Console.WriteLine("Available Locations:");
            for (int i=0; i < location.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + location[i]);
            }
            Console.Write("Enter Location Number:");
            locationNum = Convert.ToInt32(Console.ReadLine());
            return locationNum;
        }
           

        public static int GetRecTimeNum(string[] recording) //Display recording times and allows selection of one

        {
            int recordingTime = -1;
            Console.WriteLine("Applicable Recording Times:");
            for (int i = 0; i < recording.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + recording[i]);
            }
            Console.Write("Enter Recording Time:");
            recordingTime = Convert.ToInt32(Console.ReadLine());
            return recordingTime;
        }

        public static double GetWaterDepths() //Sets water depth and returns to array

        {
            double depth = 0.0;
            Console.Write("Enter depth of water:");
            depth = Convert.ToDouble(Console.ReadLine());
            return depth;
        }

        public static void DisplayTable(double[,] waterDepth, string[] location, string[] recording) //Displays table with total data

        {
            Console.WriteLine("Location\tAverage Depth");
            for (int i = 1; i < waterDepth.GetLength(0); i++)
            {
                Console.WriteLine(i + ". " + location[i - 1] + "\t" + waterDepth[i, 0]);
            }
            Console.WriteLine("Recording Time\tAverage Depth");
            for (int i = 1; i < waterDepth.GetLength(1); i++)
            {
                Console.WriteLine(i + ". " + recording[i - 1] + "\t" + waterDepth[0, i]);
            }

        }

        public static void FindAvgDepthByLocation(double[,] waterDepth) //Averages depth by location
        {
            int countOfActualData = 0;
            double total = 0;
            for (int row = 1; row < waterDepth.GetLength(0); row++)
            {
                for (int col = 1; col < waterDepth.GetLength(1); col++)
                {
                    if (waterDepth[row, col] > 0)
                    {
                        total += waterDepth[row, col];
                        countOfActualData++;
                    }
                }
                if (countOfActualData != 0) //Assigns average to 0 index of column
                {
                    waterDepth[row, 0] = total / countOfActualData;   
                    total = 0;
                    countOfActualData = 0;
                }
                else
                    waterDepth[row, 0] = 0;
            }
        }

        public static void FindAvgDepthByTime(double[,] waterDepth) //Averages depth by time

        {
            int countOfActualData = 0;
            double total = 0;
            for (int col = 1; col < waterDepth.GetLength(1); col++)
            {
                for (int row = 1; row < waterDepth.GetLength(0); row++)
                {
                    if (waterDepth[row, col] > 0)
                    {
                        total += waterDepth[row, col];
                        countOfActualData++;
                    }
                }
                if (countOfActualData != 0)
                {
                    waterDepth[0, col] = total / countOfActualData;   
                    total = 0;
                    countOfActualData = 0;
                }
                else
                    waterDepth[0, col] = 0;
            }
        }
    }
}