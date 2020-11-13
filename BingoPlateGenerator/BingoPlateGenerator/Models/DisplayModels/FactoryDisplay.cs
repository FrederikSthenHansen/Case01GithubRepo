using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoPlateGenerator.Models.DisplayModels
{
    public class FactoryDisplay

    {

         public BingoFactory MyDisplay = new BingoFactory();
        
        
        public FactoryDisplay()
        {
            MyDisplay.Batch = new List<BingoPlate>();
               for (int i=0; i < 3; i++)
            {
                MyDisplay.Batch.Add(new BingoPlate(i + 1, "test"));
            }

        }
       public void PrintToDisplay()
        {
            ;
        }
       
    }
}
