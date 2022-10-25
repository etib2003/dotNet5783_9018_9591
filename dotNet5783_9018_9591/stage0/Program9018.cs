using System;
namespace stage0
{ 
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome9018();
            Welcome9591();

            Console.ReadKey();
        }
        private static void Welcome9018()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
        static partial void Welcome9591();
    }
}








