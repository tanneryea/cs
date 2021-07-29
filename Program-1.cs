using System;

namespace CommissionSalesMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            //Constant variables for deduction and commission rates
            const decimal commissionRate = 0.07m;
            const decimal fedTaxRate = 0.18m;
            const decimal stateTaxRate = 0.03m;
            const decimal retirementRate = 0.15m;
            const decimal socialSecRate = 0.09m;

            //Following method accepts input for name and sales for the month

            static String nameInput()
            {
                Console.Write("Please enter your name: ");
                String name = Console.ReadLine();
                return name;
            }

            static decimal salesInput()
            {
                Console.Write("Please enter your monthly sales: ");
                decimal monthlySales = Convert.ToDecimal(Console.ReadLine());
                return monthlySales;
            }

            //Following methods calculate deductions from commission via rates

            static decimal commission(decimal sales)
            {
                decimal commission = sales * commissionRate;
                return commission;
            }

            static decimal federalTax(decimal commission)
            {
                decimal federalTax = commission * fedTaxRate;
                return federalTax;
            }

            static decimal stateTax(decimal monthlySales, decimal federalTax)
            {
                decimal stateTax = (monthlySales - federalTax) * stateTaxRate;
                return stateTax;
            }

            static decimal retirement(decimal commission)
            {
                decimal retirement = commission * retirementRate;
                return retirement;
            }

            static decimal socialSecurity(decimal commission)
            {
                decimal socialSecurity = commission * socialSecRate;
                return socialSecurity;
            }

            //Calculates final total from returned values of above methods

            static decimal finalTotal(decimal commission, decimal federalTax, decimal stateTax, decimal retirement, decimal socialSecurity)
            {
                decimal finalTotal = commission - federalTax - stateTax - retirement - socialSecurity;
                return finalTotal;
            }

            //Outputs formatted deduction list

            static void outputMethod(String name, decimal monthlySales, decimal commission, decimal federalTax, decimal stateTax, decimal retirement, decimal socialSecurity, decimal finalTotal)
            {
                //Prints out results of previous calculations
                Console.WriteLine("Monthly commission report for: " + name);
                Console.WriteLine("-----------------------------------------------");
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

            //Invoke methods properly to run when Main() is run

            String name = nameInput();
            decimal monthlySales = salesInput();

            decimal commis = commission(monthlySales);
            decimal fedTax = federalTax(commis);
            decimal staTax = stateTax(monthlySales, fedTax);
            decimal retire = retirement(commis);
            decimal social = socialSecurity(commis);
            decimal income = finalTotal(commis, fedTax, staTax, retire, social);

            outputMethod(name, monthlySales, commis, fedTax, staTax, retire, social, income);
        }
    }
}
