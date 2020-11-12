using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace BingoPlateGenerator.Models
{
    public class BingoPlate
    {
        public string Name;
        public int Id;

        /// <summary>
        /// Jagged array til at danne tabellens kolonner
        /// </summary>
        public int[][] Columns=new int [9][]; 
        

        public Hash _uniqueID;
        private Random RNG= new Random(DateTime.Now.Second);
        public int TotalNumbers;

       
        /// <summary>
        /// contructor for Bingoplate husk at fjerne navn og tilføje det seperat
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public BingoPlate(int id, string name)
        {
            //Loop til at udfylde alle 9 kolloner med 3 nuller
            for (int x = 0; x < Columns.Length; x++)
            {
                Columns[x] = new int[3] { 0, 0, 0 };
            }
            printLoop();

            if (TotalNumbers < 15)
            {
                printLoop();
                //skriv videre 
            }
            else
            {
                Id = id;
                Name = name;
            }
        }

        private void printLoop()
        {
            

            for (int x = 0; x < Columns.Length; x++)
            {
                int columnPrintsAllowed = 2;

                for (int y = 0; y < Columns[x].Length/*.Row.Length*/; y++)
                {
                    if (CheckSpot(y, x, columnPrintsAllowed) == true)
                    {
                        columnPrintsAllowed--;
                        TotalNumbers++;
                    }
                }
            }
        }

        private int printSpot (int rowNumber,int columnNumber, int fails)
        {
            //Random rng = new Random(RNG.Next(int.MinValue, int.MaxValue));
            int val = 0;
            int previousVal;
            
            switch (rowNumber) 
            { 
                case 0:
                    previousVal = 0;
                    break;
                default:
                    previousVal = Columns[columnNumber]/*.Row*/[rowNumber - 1];
                    break;
            }
            
            val =GenerateFieldValue(columnNumber);
              
            
         //Check at at det tidligere tal i kolonnen og kald recursivt via printSucces, 
         //hvis ikke dette virker ved 3 forsøg opgives der, maxværdi som previous value ellers skaber infinite loop..
            if (val > previousVal)
            {
                Columns[columnNumber]/*.Row*/[rowNumber] = val;
                return 0;
            }
            else if (fails>= 3)
            {
                return fails;
            }
            {
                printSucces(rowNumber,columnNumber);
                return fails;
            }


        }


        public int GenerateFieldValue(int columnNumber)
        {
            Random rng = new Random(RNG.Next(int.MinValue, int.MaxValue));
            int val = 0;
            switch (columnNumber)
            {
                case 0:
                    val = rng.Next(1, 10);
                    break;
                case 1:
                    val = rng.Next(10, 20);
                    break;
                case 2:
                    val = rng.Next(20, 30);
                    break;
                case 3:
                    val = rng.Next(30, 40);
                    break;
                case 4:
                    val = rng.Next(40, 50);
                    break;
                case 5:
                    val = rng.Next(50, 60);
                    break;
                case 6:
                    val = rng.Next(60, 70);
                    break;
                case 7:
                    val = rng.Next(70, 80);
                    break;
                case 8:
                    val = rng.Next(80, 91);
                    break;
                default:
                    val = 0;
                    break;
            }
            return val;
        }

        public int printSucces(int rowNumber, int columnNumber)
        {
           return printSpot(rowNumber, columnNumber, 0);
        }

        public bool CheckSpot(int rowNumber, int columnNumber, int remainingPrints)
        {
            bool value = false;

            //if (redo == true)
            //{
            //    genRand = 1;
            //}

            if (Columns[columnNumber]/*.Row*/[rowNumber] == 0)
            {
                Random rng = new Random(RNG.Next(int.MinValue, int.MaxValue));
                int chance = rng.Next(0, 2);

                if (chance == 1 && remainingPrints > 0)
                {
                    switch (rowNumber)
                    {
                        case 0:
                            //Logic gate til at forhindre fuld kolonne ved "genprint"
                            if (Columns[columnNumber]/*.Row*/[1] != 0 && Columns[columnNumber]/*.Row*/[2] != 0)
                            {
                                value = false;
                                break;
                            }
                            else
                            {
                                switch (printSucces(rowNumber, columnNumber))
                                {
                                    case 3:
                                        value = false;
                                        break;
                                    default:
                                        value = true;
                                        break;

                                }
                                break;
                            }

                        case 1:
                            switch (printSucces(rowNumber, columnNumber))
                            {
                                case 3:
                                    value = false;
                                    break;
                                default:
                                    value = true;
                                    break;

                            }
                            break;

                        case 2:
                            
                            switch (printSucces(rowNumber, columnNumber))
                            {
                                case 3:
                                    value = false; 
                                    break;
                                default:
                                    value = true;
                                    break;

                            }
                            break;
                           
                    }
                }
                else
                {
                    value = false;
                }
            }
            return value;
        }
    }
}
