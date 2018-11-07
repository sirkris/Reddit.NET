using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class MultisTests : BaseTests
    {
        [TestMethod]
        public void Mine()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            List<LabeledMultiContainer> mine = reddit.Models.Multis.Mine(false);

            Assert.IsNotNull(mine);
        }

        [TestMethod]
        public void MineWithExpandSrs()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            List<LabeledMultiContainer> mine = reddit.Models.Multis.Mine(true);

            Assert.IsNotNull(mine);
        }

        [TestMethod]
        public void User()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            List<LabeledMultiContainer> multis = reddit.Models.Multis.User("KrisCraig", false);

            Assert.IsNotNull(multis);
        }

        [TestMethod]
        public void Get()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            LabeledMultiContainer multi = reddit.Models.Multis.Get("user/KrisCraig/m/unitedprogressives", false);

            Assert.IsNotNull(multi);
        }

        [TestMethod]
        public void GetDescription()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            LabeledMultiDescriptionContainer description = reddit.Models.Multis.GetDescription("user/KrisCraig/m/unitedprogressives");

            Assert.IsNotNull(description);
        }

        [TestMethod]
        public void GetMultiSub()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            object res = reddit.Models.Multis.GetMultiSub("user/KrisCraig/m/unitedprogressives", "StillSandersForPres");

            Assert.IsNotNull(res);
        }
    }
}
