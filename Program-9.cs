using System;
using System.IO;
using System.Xml.Schema;

namespace Chapter12
{
    class Program
    {
        public static Fraction fr;

        static void Main(string[] args) //Main program
        {
            int choice = showMenu();
            while (choice > 0) //Prevents choice from being below 0
            {
                switch (choice)
                {
                    case 1:
                        initialize();
                        break;
                    case 2:
                        Console.WriteLine("Fraction: " + fr.toString());
                        break;
                    case 3:
                        Console.WriteLine("Reduced Form: " + fr.reducedForm());
                        break;
                }
                choice = showMenu();
            }
        }

        public static int showMenu() //Shows menu of options
        {
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Initialize Fraction");
            Console.WriteLine("2. Print Fraction");
            Console.WriteLine("3. Print Reduced Form");
            Console.Write(">>");
            string choice = Console.ReadLine();
            return int.Parse(choice);
        }

        public static void initialize() //Allows you to enter fraction information
        {
            Console.Write("Enter numerator: ");
            int num = int.Parse(Console.ReadLine());
            Console.WriteLine();

            int den = 0;            

            Console.Write("Enter denominator: ");
            den = int.Parse(Console.ReadLine());
            Console.WriteLine();

            while (den == 0)
            {
                Console.Write("Enter denominator greater than zero: ");
                den = int.Parse(Console.ReadLine());
                Console.WriteLine();
            }

            fr = new Fraction(num, den);
        }

        public class Fraction
        {
            //Setters and getters for num and den
            private int num { get; set; }
            private int den { get; set; }

            public Fraction(int num, int den) //Constructor
            {
                this.num = num;
                this.den = den;
            }

            public string reducedForm() //Reduces fraction down to its smallest form
            {
                int smaller = num < den ? num : den; //If-else constructor to determine the smaller number
                int HCF = -1; //Highest common factor
                string reducedum = "";
                for (int i = smaller; i > 0; --i)
                {
                    if (num % i == 0 && den % i == 0)
                    {
                        HCF = i;
                        reducedum = (num / HCF) + "/" + (den / HCF);
                        break;
                    }
                }
                return reducedum;
            }

            public string toString() //String override
            {
                return num + "/" + den;
            }
        }
    }
}
