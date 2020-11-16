using System;
using BingoPlateGenerator.Models;

namespace PlateConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            BingoFactory ConsoleFactory = new BingoFactory();
            CardPrinter printer = new CardPrinter();
            ConsoleFactory.PrintNewBatch("test", 20);
            foreach(var item in ConsoleFactory.Batch)
            {
                printer.printBingoToConsole(item);
            }


            Console.ReadKey();
        
        }
    }
}
