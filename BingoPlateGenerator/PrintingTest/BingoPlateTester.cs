using System;
using System.Collections.Generic;
using System.Text;
using BingoPlateGenerator.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PrintingTest
{   
    [TestClass]
    class BingoPlateTester
    {
        //Initializer
        BingoPlate TestPlate = new BingoPlate(0, "test");

        [TestMethod]
        public void MaxValueInTopField()
        {//arrange
            TestPlate.Columns[0][0] = 9;

            //act/assert
            Assert.AreEqual(3,TestPlate.printSucces(1, 0));

        }

    }
}
