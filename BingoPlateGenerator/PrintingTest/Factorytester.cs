using Microsoft.VisualStudio.TestTools.UnitTesting;
using BingoPlateGenerator.Models;

namespace PrintingTest
{
    [TestClass]
    public class Factorytester
    {
         
        
        //Initializing
       BingoFactory factory = new BingoFactory();
        BingoPlate TestPlate = new BingoPlate(0, "test");

        [TestMethod]
        public void MaxValueInTopField()
        {//arrange
            TestPlate.Columns[0][0] = 9;

            //act/assert
            Assert.AreEqual(3, TestPlate.printSucces(1, 0));

        }
        [TestMethod]
        public void PrintAmountTest()
        {
           //arrange
           int printAmount = 10;
            
            //act
            factory.PrintNewBatch("test", printAmount);

           Assert.AreEqual(printAmount, factory.Batch.Count);
        }

    }
}
