using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Things;
using System.Collections.Generic;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class MultisTests : BaseTests
    {
        public MultisTests() : base() { }

        [TestMethod]
        public void Mine()
        {
            List<LabeledMultiContainer> mine = reddit.Models.Multis.Mine(false);

            Assert.IsNotNull(mine);
        }

        [TestMethod]
        public void MineWithExpandSrs()
        {
            List<LabeledMultiContainer> mine = reddit.Models.Multis.Mine(true);

            Assert.IsNotNull(mine);
        }

        [TestMethod]
        public void User()
        {
            List<LabeledMultiContainer> multis = reddit.Models.Multis.User("KrisCraig", false);

            Assert.IsNotNull(multis);
        }

        [TestMethod]
        public void Get()
        {
            LabeledMultiContainer multi = reddit.Models.Multis.Get("user/KrisCraig/m/unitedprogressives", false);

            Assert.IsNotNull(multi);
        }

        [TestMethod]
        public void GetDescription()
        {
            LabeledMultiDescriptionContainer description = reddit.Models.Multis.GetDescription("user/KrisCraig/m/unitedprogressives");

            Assert.IsNotNull(description);
        }

        [TestMethod]
        public void GetMultiSub()
        {
            Assert.IsNotNull(reddit.Models.Multis.GetMultiSub("user/KrisCraig/m/unitedprogressives", "StillSandersForPres"));
        }
    }
}
