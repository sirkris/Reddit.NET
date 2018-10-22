using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class FlairTests : BaseTests
    {
        [TestMethod]
        public void UserFlair()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            List<Flair> flairs = reddit.Models.Flair.UserFlair("Political_Revolution");

            Assert.IsNotNull(flairs);
            Assert.IsTrue(flairs.Count > 0);
        }

        public void UserFlairV2()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            List<FlairV2> flairs = reddit.Models.Flair.UserFlairV2("Political_Revolution");

            Assert.IsNotNull(flairs);
            Assert.IsTrue(flairs.Count > 0);
        }
    }
}
