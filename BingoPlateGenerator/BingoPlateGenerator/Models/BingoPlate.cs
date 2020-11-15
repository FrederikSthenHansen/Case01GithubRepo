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
        public int stacktracer;
        int ColumnPrintsAllowed=0;
       public string UniqueId;
        int fails = 0;
        public string Name;
        public int Id;

        /// <summary>
        /// Jagged array til at danne tabellens kolonner
        /// </summary>
        public int[][] Columns=new int [9][];

        int[] Zeros = new int[] { 1,1,1};

        //public Hash _uniqueID;
        private Random RNG= new Random();
        public int TotalNumbers;

       
        /// <summary>
        /// contructor for Bingoplate. Kalder PrintLoop()
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public BingoPlate(int id, string name)
        {
            stacktracer = 0;
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

                Id = id;
                Name = name;
            UniqueId = plateToString();
            
        }


        private string plateToString()
        { string retValue = "";
            for (int x = 0; x < Columns.Length; x++)
            {
              
                for (int y = 0; y < Columns[x].Length/*.Row.Length*/; y++)
                {
                    retValue = retValue + Columns[x]/*.Row*/[y];
                }
            }
            return retValue;
        }

        /// <summary>
        /// Hoved-metode til at printe indhold i bingopladen. Kalder Checkspot
        /// </summary>
        private void printLoop()
        {
            for (int x = 0; x < Columns.Length; x++)
            {
                int[] monitorColumn = Columns[x];
                    //columnsprintsallowed = antal nuller i kollonen -1, da vi kun på skrive 2 gange pr kolonne.
               Zeros = Array.FindAll(monitorColumn, element => element.Equals(0)) ;
                ColumnPrintsAllowed = Zeros.Length-1;


                for (int y = 0; y < Columns[x].Length/*.Row.Length*/; y++)
                {
                    if (CheckSpot(y, x, ColumnPrintsAllowed) == true)
                    {
                        ColumnPrintsAllowed--;
                        TotalNumbers++;
                    }
                }
            }
            
        }

        /// <summary>
        /// metode til at checke at en genereret plade er lovlig
        /// </summary>
        private int verifyRow(int y)
        {
                int[] row=new int[9];

                for (int x = 0; x < Columns.Length; x++)
                {
                    row[x] = Columns[x]/*.Row*/[y];
                }

                int[] blanks= Array.FindAll(row, element => element.Equals(0));
                return blanks.Length;   
        }

        /// <summary>
        /// hjælpemetode der sikrer sig at det print den udfører er lovligt. Kalder GenerateFieldValue til at genere sine tal og VerifyCoilumn, til at checke om kollen har plads.
        /// </summary>
        /// <param name="rowNumber"></param>
        /// <param name="columnNumber"></param>
        /// <returns>Int "fails"</returns>
        private int printSpot (int rowNumber,int columnNumber)
        {
            //Random rng = new Random(RNG.Next(int.MinValue, int.MaxValue));
            
            int val = 0;
            int previousVal=0;
            int furtherPreviousVal = -1;
            int nextVal=109;
            int furtherNextVal=110;
            
            // switch til at forindre nulrefferences
            switch (rowNumber) 
            { 
                case 0:
                    
                    nextVal = Columns[columnNumber]/*.Row*/[rowNumber + 1];
                    furtherNextVal = Columns[columnNumber]/*.Row*/[rowNumber + 2];

                    //tjek om de næste værdier er 0
                    if (nextVal == 0) { nextVal = 109; }
                    if (furtherNextVal == 0) { furtherNextVal = 110; };
                    break;
                case 1:
                    previousVal = Columns[columnNumber]/*.Row*/[rowNumber - 1];
                    nextVal = Columns[columnNumber]/*.Row*/[rowNumber + 1];
                    if (nextVal == 0) { nextVal = 110; }
                    break;

                case 2:
                    previousVal = Columns[columnNumber]/*.Row*/[rowNumber - 1];
                    furtherPreviousVal = Columns[columnNumber]/*.Row*/[rowNumber - 2];
                    if (furtherPreviousVal == 0) { furtherPreviousVal = -1; }
                    break;
               
            }

            //check at der ikke er skrevet 5 tal i rækken endnu
            if (verifyRow(rowNumber) >4)
            {
                val = GenerateFieldValue(columnNumber);

                //Check at at det tidligere tal i kolonnen er lavere, samt det næste er højere og kald recursivt via printSucces, hvis ikke dette virker.
                // Ved 3 forsøg opgives der, maxværdi som previous value ellers skaber infinite loop.. ligningsudgaver står over hver IF-else statement

                // prevVal<val<nextval<furthernextval
                if (val > previousVal && previousVal>furtherPreviousVal && val < nextVal && nextVal < furtherNextVal && ColumnPrintsAllowed > 0)
                {
                    Columns[columnNumber]/*.Row*/[rowNumber] = val;
                    fails = 0;
                    return 0;
                }

                //nextval=0  Furthernextval>val>previousval 
                else if (val > previousVal && previousVal > furtherPreviousVal && nextVal == 0 && val < furtherNextVal && ColumnPrintsAllowed > 0)
                {
                    Columns[columnNumber]/*.Row*/[rowNumber] = val;
                    fails = 0;
                    return 0;
                }

                //val>prevVal next+futhernext =0
                else if (val > previousVal && previousVal > furtherPreviousVal && nextVal == 0 && furtherNextVal == 0 && ColumnPrintsAllowed > 0)
                {
                    Columns[columnNumber]/*.Row*/[rowNumber] = val;
                    fails = 0;
                    return 0;
                }

                //nextval>val>prevVal og furthernextval=0
                else if (val > previousVal && previousVal > furtherPreviousVal && val < nextVal && furtherNextVal == 0 && ColumnPrintsAllowed > 0)
                {
                    Columns[columnNumber]/*.Row*/[rowNumber] = val;
                    fails = 0;
                    return 0;
                }

                else if (val<previousVal && furtherNextVal==0||previousVal==0 && furtherPreviousVal < val)
                {
                    Columns[columnNumber]/*.Row*/[rowNumber] = val;
                    fails = 0;
                    return 0;
                }

                else if (/*fails >= 3 ||*/ stacktracer >= 10)
                {
                    return fails = 3;
                }
                else
                {
                    stacktracer++;
                    printSucces(rowNumber, columnNumber);
                    return fails;
                }
            }
            else { return 3; }

        }

        /// <summary>
        /// Hjælpemetode til at generere de korrekte tilfældige tal til bingopladen, ud fra hvilken kolonne vi befinder os i. 
        /// Returnerer tallet til at blive printet
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns>int value to be printed</returns>
        public int GenerateFieldValue(int columnNumber)
        {
            Random rng = new Random(RNG.Next());
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


        /// <summary>
        /// Hjælpemetode til igangsætte print i et felt, og sikre sig at printet er lovligt. Hvis returværdien er 3, har der været 3 fejlslagne forsøg på at printe.
        /// Kalder printSpot()
        /// </summary>
        /// <param name="rowNumber"></param>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        public int printSucces(int rowNumber, int columnNumber)
        {
           return printSpot(rowNumber, columnNumber);
        }

        /// <summary>
        /// Hjælpemetode til at afgøre om der skal printes i et felt. Tager som parametre række og colonne numbre, samt den tilladte mængde print i kolonnen.
        /// Kalder printSucces
        /// </summary>
        /// <param name="rowNumber">int</param>
        /// <param name="columnNumber">int</param>
        /// <param name="remainingPrints">int</param>
        /// <returns>bool</returns>
        public bool CheckSpot(int rowNumber, int columnNumber, int remainingPrints)
        {
            stacktracer = 0;
            bool value = false;

            //hvis feltet er tomt:
            if (Columns[columnNumber]/*.Row*/[rowNumber] == 0)
            {
                Random rng = new Random(RNG.Next(int.MinValue, int.MaxValue));
                int chance = rng.Next(0, 2);

                //hvis vi ikke har skrevet i øverste felt, vil vi som udgangspunkt gerne skrive.
                if (rowNumber >0 && remainingPrints==2 || rowNumber == 2 && remainingPrints > 0) 
                { chance = 1; }

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

                        default:
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
