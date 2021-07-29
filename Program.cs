using System;

namespace CommissionSales
{
    class Program
    {
        static void Main(string[] args)
        {
            //Constant variables for deduction and commission rates
            const decimal commissionRate = 0.07m;
            const decimal fedTaxRate = 0.18m;
            const decimal stateTaxRate = 0.03m;
            const decimal retirementRate = 0.1m;
            const decimal socialSecRate = 0.06m;

            //Compile-time variable for employee name
            string employeeName = "Joshua Montain";

            //Input for monthly sales
            Console.WriteLine("Hello, " + employeeName + "! Please input your sale for this month");
            decimal monthlySales = Convert.ToDecimal(Console.ReadLine());

            //Performs calculations of deductions, then subtracts deductions from commission to get income
            decimal commission = monthlySales * commissionRate;
            decimal federalTax = commission * fedTaxRate;
            decimal stateTax = (monthlySales - federalTax) * stateTaxRate;
            decimal retirement = commission * retirementRate;
            decimal socialSecurity = commission * socialSecRate;
            decimal finalTotal = commission - federalTax - stateTax - retirement - socialSecurity;

            //Prints out results of previous calculations
            Console.WriteLine("{1,20} \t\t{0,10:C}", monthlySales, "Monthly Sales: ");
            Console.WriteLine("{1,20} \t\t{0,10:P}", commissionRate, "Commission Rate: ");
            Console.WriteLine("{1,20} \t\t{0,10:N2}", commission, "Monthly Commission: ");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("{1,20} \t\t{0,10:N2}", federalTax, "Federal Tax: ");
            Console.WriteLine("{1,20} \t\t{0,10:N2}", stateTax, "State Tax: ");
            Console.WriteLine("{1,20} \t\t{0,10:N2}", retirement, "Retirement: ");
            Console.WriteLine("{1,20} \t\t{0,10:N2}", socialSecurity, "Social Security: ");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("{1,20} \t\t{0,10:C}", finalTotal, "Total Income: ");




        }
    }
}
