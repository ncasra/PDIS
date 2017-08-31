using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDIS.DataAccess;
using System.Data.Entity;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        

        [TestMethod]
        public void TestMethod1()
        {
            Class1 dataAcc = new Class1();
            dataAcc.testfunction();
            Assert.AreEqual(1,1);
        }
    }
}
