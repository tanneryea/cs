using System;

namespace RectangleAndTriangle
{
    class Program
    {
        static void Main(string[] args)
        {
            //Accepts rectangle parameters and then calculates them
            static void rectangle(double length, double width)
            {
                double area = length * width;
                double perimeter = (length + width) * 2;

                //Calls output method to print results
                output(area, "area", "rectangle");
                output(perimeter, "perimeter", "rectangle");
            }
            
            //Accepts triangle parameters and then calculates them
            static void triangle(double height, double side1, double side2, double side3)
            {
                double area = (side1 * height) / 2;
                double perimeter = side1 + side2 + side3;

                //Calls output method to print results
                output(area, "area", "triangle");
                output(perimeter, "perimeter", "triangle");
            }

            //Input for rectangle parameters
            static void rectangleInput()
            {
                Console.Write("Enter the length of the rectangle: ");
                double length = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter the width of the rectangle: ");
                double width = Convert.ToDouble(Console.ReadLine());

                //Calls rectangle calculation method
                rectangle(length, width);
            }

            //Input for triangle parameters
            static void triangleInput()
            {
                Console.Write("Enter the height of the triangle: ");
                double height = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter the longest side of the triangle: ");
                double side1 = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter the second side of the triangle: ");
                double side2 = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter the final side of the triangle: ");
                double side3 = Convert.ToDouble(Console.ReadLine());

                //Calls triangle calculation method
                triangle(height, side1, side2, side3);
            }

            //Output to display results of calculations
            static void output(double result, string calc, string shape)
            {
                Console.WriteLine("The " + calc + " of the " + shape + " is {0:F1}.", result); 
            }

            //While menu loop adapted from my C++ knowledge
            bool exitMenu = false;
            while (!exitMenu)
            {
                Console.WriteLine("Choose to calculate the area and perimeter of one of these shapes: ");
                Console.WriteLine("1. Rectangle");
                Console.WriteLine("2. Triangle");
                Console.WriteLine("3. Exit Program");
                Console.Write(">> ");

                int choice = Convert.ToInt32(Console.ReadLine());
                
                //Input validation loop
                while (choice < 1 || choice > 3)
                {
                    Console.Write("Invalid menu choice. Please enter another: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                }

                //Switch menu that allows multiple entries
                switch (choice)
                {
                    case 1:
                        rectangleInput();
                        break;
                    case 2:
                        triangleInput();
                        break;
                    case 3:
                        exitMenu = true;
                        break;
                }


            } 
        }
    }
}
