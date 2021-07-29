using System;
using System.Text.RegularExpressions;

namespace Chapter6CS
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creates variables used throughout program
            Regex regex = new Regex(@"^[0-9A-F]+$"); //Accepts only 0-9 and A-F as valid inputs
            String input;
            int inputNumber;
            int sum = 0;
            string completeInput = "";


            do //Displays the entry at least once
            {
                Console.Write("Enter a value between 0-9 or A-F. Enter Z to stop adding values: ");
                input = Console.ReadLine();

                if (!input.ToUpper().Equals("Z") && ValidateInput(input, regex)) //Runs if Z is not entered and the input is valid
                {
                    sum += Convert.ToInt32(input, 16); //Adds value of hexadecimal to accumulator

                    if (int.TryParse(input, out inputNumber)) //If it can parse a decimal, it converts it to a string value
                    {
                        completeInput += "Decimal value " + input + " has HEX value " + inputNumber.ToString("X") + "\n";
                    } else
                    {
                        completeInput += "Hex value " + input.ToString().ToUpper() + " has DECIMAL value " + Convert.ToInt32(input, 16) + "\n";
                    }
                }      
                   

            } while (!input.ToUpper().Equals("Z"));

            //Displays all entries, decimal sum and hex sum
            Console.WriteLine(completeInput);
            Console.WriteLine("Decimal Sum: {0}", sum.ToString());
            Console.WriteLine("Hex Sum: {0}", sum.ToString("X"));

        }

        public static bool ValidateInput(string input, Regex regex) //Validates that value entered is either 0-9 or A-F
        {
            if (input.Length > 1)
            {
                return false;
            } else if (!regex.IsMatch(input.ToUpper()))
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}
