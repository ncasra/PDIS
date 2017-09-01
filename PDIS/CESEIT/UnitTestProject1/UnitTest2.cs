using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDIS.DataAccess;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            OrderRepository repo = new OrderRepository();
            repo.CreateInternalOrder("1", 10.0, "Kapstaden", "St. Helena", "WEAPONS", 5.0);
        }
    }
}
