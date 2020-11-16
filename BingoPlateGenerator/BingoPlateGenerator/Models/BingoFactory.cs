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
                BingoPlate Plate = new BingoPlate(i + 1, name);

                if (VerifyPlate(Plate) == true)
                {
                    Batch.Add(Plate);
                }
                else { i--; }       
            }
        }

        public bool VerifyPlate(BingoPlate newPlate)
        { bool retValue = false;
            if (Batch.Count == 0)
            {
                retValue = true;
            }
            else
            {
                foreach (BingoPlate plate in Batch)
                {
                    if (plate == null)
                    { retValue = true; }
                    if (newPlate.UniqueId != plate.UniqueId)
                    {
                        retValue = true;
                    }

                    //hvis Unik ID er det samme fordi jeg tester en plade imod sig selv
                    else if (newPlate.UniqueId == plate.UniqueId && newPlate.Id == plate.Id)
                    { retValue = true; }
                    else
                    {
                        retValue = false;
                    }
                }
            }
            return retValue;
        }

        public BingoFactory()
        {
            Batch = new List<BingoPlate>();
        }

    }
}