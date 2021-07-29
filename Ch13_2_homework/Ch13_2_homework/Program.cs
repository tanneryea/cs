using System;
using System.IO;

namespace Ch13_2_homework
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter the file path you want to read or write to. To read the file, enter 'read' after the path.");
                return 1;
            }

            if (args.Length == 1)
            {
                enterValues(args[0]);
                return 0;
            }

            if (args.Length == 2 && args[1] == "read")
            {
                displayValues(args[0]);
                return 0;
            }

            return 0;

        }

        static void enterValues(String fileName)
        {
            if (File.Exists(fileName))
            {
                Console.WriteLine("That file exists! Please re-run program with read argument.");
                return;
            }
            bool addValues = true;
            FileStream filStream = new FileStream(fileName, FileMode.CreateNew);
            BinaryWriter binWriter = new BinaryWriter(filStream);

            do
            {
                Console.Write("Enter 1 to enter values or enter 0 to quit: ");
                int response = Convert.ToInt32(Console.ReadLine());
               

                while (response != 1 && response != 0)
                {
                    Console.Write("Invalid value. Enter 1 to enter values or enter 0 to quit:");
                    response = Convert.ToInt32(Console.ReadLine());
                }

                switch (response) {
                    case 1:                
                        int[] values = new int[6];
                        int total = 0;
                        string fileInput = "";

                        for (int i = 0; i < (values.Length - 1); i++)
                        {
                            Console.WriteLine("Enter value " + (i + 1) + ":");
                            int value = Convert.ToInt32(Console.ReadLine());
                            total += value;
                            values[i] = value;
                        }                        

                        values[5] = (total / 5);

                        foreach (int value in values)
                        {
                            fileInput += Convert.ToString(value) + " ";
                        }

                        binWriter.Write(fileInput);
                        break;
                    case 0:
                        addValues = false;
                        break;
                }
                    
            }
            while (addValues);
            binWriter.Close();
            filStream.Close();
        }

        static void displayValues(string fileName)
        {
            FileStream filStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader binReader = new BinaryReader(filStream);
            while (binReader.PeekChar() > 0)
            {
                Console.WriteLine(binReader.ReadString());
            }

            binReader.Close();
            filStream.Close();
        }
    }
}
