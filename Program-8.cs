using System;

namespace Chapter11
{
    class Program
    {
        interface IPrintable //Interface to overwite availability in each class
        {
            string availability();
        }

        class ReadingMaterial //Base class for all reading material
        {
            private string author;
            private string title;
            private double cost;

            public ReadingMaterial(string author, string title, double cost)
            {
                this.author = author;
                this.title = title;
                this.cost = cost;
            }

            public override string ToString()
            {
                return "\nTitle: " + title + "\nAuthor: " + author + "\nCost: $" + cost;
            }

        }

        class Online: ReadingMaterial, IPrintable //Online class that combines ReadingMaterial and Interface
        {
            private string website;

            public Online(string author, string title, double cost, string website):base(author, title, cost)
            {
                this.website = website;
            }

            public string availability()
            {
                return "Printable PDF";
            }

            public override string ToString()
            {
                return base.ToString() + "\nWebsite: " + website + "\nAvailabiliy: " + availability();
            }
        }

        class Book: ReadingMaterial, IPrintable //Book class that combines ReadingMaterial and Interface
        {
            private string isbn;

            public Book(string author, string title, double cost, string isbn):base(author, title, cost)
            {
                this.isbn = isbn;
            }

            public string availability()
            {
                return "From the publisher or your favorite book store";
            }

            public override string ToString()
            {
                return base.ToString() + "\nISBN: " + isbn + "\nAvailability: " + availability();
            }
        }

        class Magazine : ReadingMaterial, IPrintable //Magazine class that combines ReadingMaterial and Interface
        {
            private string date;

            public Magazine(string author, string title, double cost, string date):base(author, title, cost)
            {
                this.date = date;
            }

            public string availability()
            {
                return "From the publisher or your favorite book store";
            }

            public override string ToString()
            {
                return base.ToString() + "\nPublication Date: " + date + "\nAvailability: " + availability();
            }
        }

        public class Presentation //Main driver class that creates an object of each type
        {
            public static void Main()
            {
                Online o = new Online("Barbara Doyle", "C# Programming: From Problem Analysis to Program Design, 5th Edition", 150.00, "cengage.com");
                Console.WriteLine(o.ToString());

                Book b = new Book("James S.A. Corey", "Leviathan Wakes", 30.00, "978-0-316-12908-4");
                Console.WriteLine(b.ToString());

                Magazine m = new Magazine("Games Workshop", "White Dwarf", 15.00, "November 2020");
                Console.WriteLine(m.ToString());
            }
        }











    }
}
