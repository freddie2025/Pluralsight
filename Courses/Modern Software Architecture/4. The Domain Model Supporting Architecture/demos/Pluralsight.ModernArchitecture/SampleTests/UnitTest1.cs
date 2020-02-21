using Microsoft.VisualStudio.TestTools.UnitTesting;
using Module4.DomainModel.Business;

namespace SampleTests
{
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var match = new Match("12345", "Home", "Visitors");
            match.Start()
                .StartPeriod()
                .Goal(TeamId.Home)
                .Goal(TeamId.Home)
                .EndPeriod()
                .StartPeriod()
                .Goal(TeamId.Visitors)
                .EndPeriod()
                .StartPeriod()
                .EndPeriod()
                .StartPeriod()
                .Goal(TeamId.Home)
                .EndPeriod()
                .Finish();
            Assert.AreEqual(new Score(3, 1), match.CurrentScore);
        }
    }
}
