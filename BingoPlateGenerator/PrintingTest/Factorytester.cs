using Microsoft.VisualStudio.TestTools.UnitTesting;
using BingoPlateGenerator.Models;
using System.Collections.Generic;

namespace PrintingTest
{
    [TestClass]
    public class Factorytester
    {
         
        
        //Initializing
       BingoFactory factory = new BingoFactory();
        BingoPlate TestPlate = new BingoPlate(0, "Test Plate 1");

        [TestMethod]
        public void MaxValueInTopField()
        {//arrange
            TestPlate.Columns[0][0] = 9;

            //act/assert
            Assert.AreEqual(3, TestPlate.printSucces(1, 0));

        }
        [TestMethod]
        public void VerifyPlateTestIdenticalPlatesNotAdded()
        {//arrange
            BingoPlate testPlate2 = new BingoPlate(2, "Test Plate 2");
            factory.Batch.Add(TestPlate);
            int previousBatchcount = factory.Batch.Count;
            testPlate2.UniqueId = TestPlate.UniqueId;


            //act
            factory.VerifyPlate(testPlate2);
            if (factory.VerifyPlate(testPlate2) == true)
            {
                factory.Batch.Add(testPlate2);
            }

            //assert
            Assert.IsTrue(factory.Batch.Count==previousBatchcount);

        }


        [TestMethod]
        public void WriteToMiddleField()
        {//arrange
            TestPlate.Columns[0][0] = 1;

            //act
            TestPlate.printSucces(1, 0);

            //assert
            Assert.IsTrue(TestPlate.Columns[0][2] != 0);

        }

        [TestMethod]
        public void WriteToBottomField()
        {//arrange
            TestPlate.Columns[0][0] = 1;

            //act
            TestPlate.printSucces(2,0);

            //assert
            Assert.IsTrue(TestPlate.Columns[0][2]!=0);

        }


        /// <summary>
        /// Tester om den printer det rigtige antal. Ligenu printer den 11 når man beder om 10.
        /// </summary>
        [TestMethod]
        public void PrintAmountTest()
        {
           //arrange
           int printAmount = 10;

            //act
            factory.Batch = new List<BingoPlate>();
            factory.PrintNewBatch("test", printAmount);

           Assert.AreEqual(printAmount, factory.Batch.Count);
        }

        [TestMethod]
        public void AllBatchIsUnique()
        {
            //arrange
            int printAmount = 10;
            bool result = true;

            //act
            factory.Batch = new List<BingoPlate>();
            factory.PrintNewBatch("test", printAmount);

            //assert

            //kunne ikke finde en måde at checke en færdiglavet collection med verifyPlate, 
            //da pladerne altid tjekker 1 gang imod sig selv og failer.
            foreach (BingoPlate plate in factory.Batch)
            {
                result = factory.VerifyPlate(plate);
            }

            Assert.IsTrue(result);
        }

    }
}
