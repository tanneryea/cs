using System;

namespace Chapter5CS
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creates new object to store fuel
            Fuel amount = new Fuel();
            Console.Write("Enter the fuel price per liter for Canadian Fuel: "); //Entry for Canadian price
            double price_liter = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter the fuel price per gallon for American Fuel: "); //Entry for American price
            double price_gallon = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter fuel stop: \n <1> Canadian Fuel \n <2> American Fuel"); //Entry to determine how to convert
            int location = Convert.ToInt16(Console.ReadLine());

            switch (location)
            {
                case 1: //Conversion if in Canada
                    if (price_liter < amount.literConversion(price_gallon)) {
                        Console.WriteLine("The price at Canadian Fuel is the most economical fuel price!");
                    } else if (price_liter > amount.literConversion(price_gallon)) {
                        Console.WriteLine("The price at American Fuel is the most economical fuel price!");
                    } else {
                        Console.WriteLine("The two prices are the same!");
                    }
                    Console.WriteLine("The price per liter at Canadian Fuel is $" + price_liter + "\n The price per liter at American Fuel is $" + amount.literConversion(price_gallon));
                    break;
                case 2: //Conversion if in America
                    if (price_gallon > amount.gallonConversion(price_liter))
                    {
                        Console.WriteLine("The price at Canadian Fuel is the most economical fuel price!");
                    }
                    else if (price_gallon < amount.gallonConversion(price_liter))
                    {
                        Console.WriteLine("The price at American Fuel is the most economical fuel price!");
                    }
                    else
                    {
                        Console.WriteLine("The two prices are the same!");
                    }
                    Console.WriteLine("The price per gallon at American Fuel is $" + price_gallon + "\n The price per gallon at Canadian Fuel is $" + amount.gallonConversion(price_liter));
                    break;
            }

        }
       

                   
    }

    class Fuel //Simple conversion class
    {
        public double literConversion(double price_gallon)
        {
            return price_gallon * 0.264172;
        }

        public double gallonConversion(double price_liter)
        {
            return price_liter / 0.264172;
        }
    }
}
