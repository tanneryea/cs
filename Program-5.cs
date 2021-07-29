using System;

namespace Chapter7HW
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] FloridaAreaCodes = { 239, 305, 321, 352, 386, 407, 448, 561, 656, 689, 727, 754, 772, 786, 813, 850, 863, 904, 941, 954 };

            AreaCode ob1 = new AreaCode(FloridaAreaCodes);
            ob1.AreaCodeSearch(850);
            Console.WriteLine(ob1.ToString());

        }
    }

    class AreaCode
    {
        private int[] AreaCodes;

        public AreaCode(int [] inputList)
        {
            AreaCodes = inputList;
        }

        public Boolean AreaCodeSearch(int SearchCode)
        {
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
