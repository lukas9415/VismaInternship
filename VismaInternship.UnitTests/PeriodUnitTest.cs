using Microsoft.VisualStudio.TestTools.UnitTesting;
using VismaInternship.Models;

namespace VismaInternship.UnitTests
{
    [TestClass]
    public class PeriodUnitTest
    {
        [TestMethod]
        public void Period_Start_UnitTest()
        {
            Period period = new Period();
            period.Start = System.DateTime.Today;
            Assert.IsTrue(period.Start.Equals(System.DateTime.Today));
        }
        [TestMethod]
        public void Period_End_UnitTest()
        {
            Period period = new Period();
            period.End = System.DateTime.Today;
            Assert.IsTrue(period.End.Equals(System.DateTime.Today));
        }
        [TestMethod]
        public void Period_IntersectsWith_DoesIntersect_UnitTest()
        {
            Period period1 = new Period();
            Period period2 = new Period();
            period1.Start = System.DateTime.Parse("2022-01-01");
            period1.End = System.DateTime.Parse("2022-01-03");

            period2.Start = System.DateTime.Parse("2022-01-02");
            period2.End = System.DateTime.Parse("2022-01-03");
            Assert.IsTrue(period1.IntersectsWith(period2));
        }
        [TestMethod]
        public void Period_IntersectsWith_DoesNotIntersect_UnitTest()
        {
            Period period1 = new Period();
            Period period2 = new Period();
            period1.Start = System.DateTime.Parse("2022-01-01");
            period1.End = System.DateTime.Parse("2022-01-03");

            period2.Start = System.DateTime.Parse("2022-02-02");
            period2.End = System.DateTime.Parse("2022-02-03");
            Assert.IsFalse(period1.IntersectsWith(period2));
        }

    }
}