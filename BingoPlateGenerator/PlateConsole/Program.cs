using System;
using BingoPlateGenerator.Models;

namespace PlateConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            BingoFactory ConsoleFactory = new BingoFactory();
            ConsoleFactory.PrintNewBatch("test", 5);



        }
    }
}
