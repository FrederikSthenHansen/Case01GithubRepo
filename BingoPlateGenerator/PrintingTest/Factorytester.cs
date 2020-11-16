using Microsoft.VisualStudio.TestTools.UnitTesting;
using BingoPlateGenerator.Models;
using System.Collections.Generic;
using System;

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
            if (factory.VerifyPlate(testPlate2) == 5)
            {
                factory.Batch.Add(testPlate2);
            }

            //assert
            Assert.IsTrue(factory.Batch.Count==previousBatchcount);

        }


        [TestMethod]
        public void AllPlatesAreLegal()
        {
            //arrange
            int printAmount = 10;
            bool resultVerifyRow= true;
            bool resultTotalNumbers = true;

            //act
            factory.Batch = new List<BingoPlate>();
            factory.PrintNewBatch("test", printAmount);

            foreach(BingoPlate plate in factory.Batch)
            { if (plate.TotalNumbers != 15) { resultTotalNumbers = false; }
                for (int y = 0; y < 3; y++)
                {
                    if (plate.VerifyRow(y) != 4)
                        resultVerifyRow = false; 
                }
            }
            
            Assert.IsTrue(resultTotalNumbers );
            Assert.IsTrue(resultVerifyRow);
        }

        [TestMethod]
        public void WriteToMiddleFieldInEmptyColumn()
        {//arrange
            for (int x = 0; x < TestPlate.Columns.Length; x++) { TestPlate.Columns[x] = new int[] { 0, 0, 0 }; }

            TestPlate.Columns[0][0] = 0;
            TestPlate.Columns[0][1] = 0;
            TestPlate.Columns[0][2] = 0;

            //act
            TestPlate.printSucces(1, 0);

            //assert
            Assert.IsTrue(TestPlate.Columns[0][1] != 0);

        }

        [TestMethod]
        public void WriteToBottomField()
        {//arrange
            for (int x = 0; x < TestPlate.Columns.Length; x++) { TestPlate.Columns[x] = new int[] { 0, 0, 0 }; }

            TestPlate.Columns[0][0] = 1;
            TestPlate.Columns[0][1] = 0;


            //act
            TestPlate.printSucces(2,0);

            //assert
            Assert.IsTrue(TestPlate.Columns[0][2]!=0);

        }

        [TestMethod]
        public void DontWriteToBottomField()
        {//arrange
            TestPlate.Columns[0][0] = 1;
            TestPlate.Columns[0][1] = 2;
            TestPlate.Columns[0][2] = 0;
            int[] Zeros = Array.FindAll(TestPlate.Columns[0], element => element.Equals(0));
            TestPlate.ColumnPrintsAllowed = Zeros.Length - 1;

            //act
            TestPlate.CheckSpot(2,0, TestPlate.ColumnPrintsAllowed);

            //assert
            Assert.IsTrue(TestPlate.Columns[0][2] == 0);

        }

        [TestMethod]
        public void DontWriteToMiddleField()
        {//arrange
            TestPlate.Columns[0][0] = 1;
            TestPlate.Columns[0][1] = 0;
            TestPlate.Columns[0][2] = 2;
            int[] Zeros = Array.FindAll(TestPlate.Columns[0], element => element.Equals(0));
            TestPlate.ColumnPrintsAllowed = Zeros.Length - 1;
            //act
            TestPlate.CheckSpot(1, 0, TestPlate.ColumnPrintsAllowed);

            //assert
            Assert.IsTrue(TestPlate.Columns[0][1] == 0);

        }

        [TestMethod]
        public void DontWriteToTopField()
        {//arrange
            TestPlate.Columns = new int[9][];

            for(int x=0; x<TestPlate.Columns.Length;x++) { TestPlate.Columns[x] = new int[] { 0, 0, 0 }; }

            TestPlate.Columns[0][0] = 0;
            TestPlate.Columns[0][1] = 1;
            TestPlate.Columns[0][2] = 2;
            int[] Zeros = Array.FindAll(TestPlate.Columns[0], element => element.Equals(0));
            TestPlate.ColumnPrintsAllowed = Zeros.Length - 1;
            //act
            TestPlate.CheckSpot(0, 0, TestPlate.ColumnPrintsAllowed);

            //assert
            Assert.IsTrue(TestPlate.Columns[0][0] == 0);

        }

        [TestMethod]
        public void ColumnPrintsAllowedWorks()
        {//arrange
            TestPlate.Columns[0][0] = 0;
            TestPlate.Columns[0][1] = 0;
            TestPlate.Columns[0][2] = 0;

            for (int x = 0; x < TestPlate.Columns.Length; x++) { TestPlate.Columns[x] = new int[] { 0, 0, 0 }; }

            int[] Zeros = Array.FindAll(TestPlate.Columns[0], element => element.Equals(0));
            TestPlate.ColumnPrintsAllowed = Zeros.Length - 1;
            //act
            for (int y = 0; y < 3; y++)
            {

                if (TestPlate.CheckSpot(y, 0, TestPlate.ColumnPrintsAllowed) == true) 
                { TestPlate.ColumnPrintsAllowed = Zeros.Length - 1; }
            }
            

            //assert
            Assert.IsTrue(TestPlate.ColumnPrintsAllowed == 0);

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
                if(factory.VerifyPlate(plate) == 5) { result = true; }
                else { result = false; }
            }

            Assert.IsTrue(result);
        }

    }
}
