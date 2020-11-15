using System;
using System.Collections.Generic;
using System.Text;
using BingoPlateGenerator.Models;

namespace PlateConsole
{
    class CardPrinter
    {
        BingoFactory factory = new BingoFactory();

        public void printBingoToConsole(BingoPlate plate)
        { const string vertDivider = "|";
            const string Divider1 = "*---------------------------";
            const string Divider2 = "-------------------------------*";
            string print = "";
            Console.WriteLine($"unique ID string: {plate.UniqueId}");
            Console.WriteLine(Divider1+plate.Name+Divider2);
            for (int y = 0; y <3 /*plate.Columns.Length*/; y++)
            {
                for (int x = 0; x < 9/*plate.Columns.Length*/; x++)
                {
                    string number = plate.Columns[x][y].ToString();
                    if (number.Length < 2) { number = "0" + number; }
                    print = print+vertDivider+number+vertDivider;
                }
                Console.WriteLine(print);
                print = "";
            }
            Console.WriteLine(Divider1+plate.Id+Divider2);
            Console.WriteLine();
        }
    }
}
    
    

