using System;

namespace Chapter7HW
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] FloridaAreaCodes = { 239, 305, 321, 352, 386, 407, 448, 561, 656, 689, 727, 754, 772, 786, 813, 850, 863, 904, 941, 954 };
            int[] AlabamaAreaCodes = { 205, 334, 256, 251, 659, 938 };
            int[] GeorgiaAreaCodes = { 229, 404, 470, 478, 706, 678, 762, 770, 912 };

            AreaCode Florida = new AreaCode(FloridaAreaCodes);
            Console.WriteLine("Doing a search through Florida...");
            Florida.AreaCodeSearch();
            Console.WriteLine(Florida.ToString());

            AreaCode Alabama = new AreaCode(AlabamaAreaCodes);
            Console.WriteLine("Doing a search through Alabama...");
            Alabama.AreaCodeSearch();
            Console.WriteLine(Alabama.ToString());

            AreaCode Georgia = new AreaCode(GeorgiaAreaCodes);
            Console.WriteLine("Doing a search through Georgia...");
            Georgia.AreaCodeSearch();
            Console.WriteLine(Georgia.ToString());

        }
    }

    class AreaCode
    {
        private int[] AreaCodes;

        public AreaCode(int [] inputList)
        {
            AreaCodes = inputList;
        }

        public Boolean AreaCodeSearch()
        {
            Console.WriteLine("Enter the area code you want to search for: ");
            int SearchCode = int.Parse(Console.ReadLine());
            if (Array.BinarySearch(AreaCodes, SearchCode) >= 0)
            {
                Console.WriteLine(SearchCode + " was found in the Area Code list!");
                return true;
            } else
            {
                Console.WriteLine(SearchCode + " was NOT found in the Area Code list!");
                return false;
            }
        }

        public override string ToString()
        {
            string AreaCodeList = "";
            foreach (int code in AreaCodes)
            {
                AreaCodeList += "(" + code + ")\n";
            }
            return AreaCodeList;
        }
    }
}
