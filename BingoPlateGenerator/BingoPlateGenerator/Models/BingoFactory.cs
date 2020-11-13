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
            int leftToPrint = amount;

            for (int i = Batch.Count; i <= Batch.Count + leftToPrint; i++)
            {
                BingoPlate Pl8 = new BingoPlate(i - 1, name);

                Batch.Add(Pl8);
            }

        }

        public BingoFactory()
        {
            Batch = new List<BingoPlate>();
        }

    }
}