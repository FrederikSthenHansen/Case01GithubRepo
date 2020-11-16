using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoPlateGenerator.Models
{
    public class BingoFactory
    {
        public List<BingoPlate> Batch = new List<BingoPlate>();



        public void PrintNewBatch(string name, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                BingoPlate newPlate = new BingoPlate(i + 1, name);

                checkAndAdd(newPlate);
                       
            }
        }
        private void checkAndAdd(BingoPlate plate)
        {
            int rowToFix =600;
            rowToFix = VerifyPlate(plate);
            if (rowToFix == 5)
            {
                Batch.Add(plate);
            }
            else 
            { 
                for (int x = 0; x < 9; x++) { plate.printSucces(rowToFix,x); }
                //checkAndAdd(plate);
                
            } 
                
        }

        private int allRowsAreLegal(BingoPlate plate)
        { int retvalue = 5;
            for (int y=0;y<3; y++)
            {
                if (plate.VerifyRow(y) != 4)
                {
                    retvalue =y;
                }
            }
            return retvalue;
        }

        public int VerifyPlate(BingoPlate newPlate)
        { int retValue = 0;
            if (Batch.Count == 0)
            {
                retValue = 5;
            }
            else
            {
                foreach (BingoPlate plate in Batch)
                {
                    if (plate == null)
                    { retValue = 5; }

                    //er UnikID forskellig?
                    if (newPlate.UniqueId != plate.UniqueId)
                    {
                        retValue = allRowsAreLegal(plate);
                    }

                    //hvis Unik ID er det samme fordi jeg tester en plade imod sig selv
                    else if (newPlate.UniqueId == plate.UniqueId && newPlate.Id == plate.Id)
                    {
                        retValue = allRowsAreLegal(plate);
                    }
                    else
                    {
                        
                        retValue = 404;
                    }
                }
            }
            //så vi kan se pladerne
            retValue = 5;
            return retValue;
        }

        public BingoFactory()
        {
            Batch = new List<BingoPlate>();
        }

    }
}