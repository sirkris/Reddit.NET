using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Inputs.Flair;
using Reddit.Things;
using System;
using System.Collections.Generic;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class FlairTests : BaseTests
    {
        public FlairTests() : base() { }

        [TestMethod]
        public void UserFlair()
        {
            List<Flair> flairs = reddit.Models.Flair.UserFlair(testData["Subreddit"]);

            Validate(flairs);
        }

        [TestMethod]
        public void UserFlairV2()
        {
            List<FlairV2> flairs = reddit.Models.Flair.UserFlairV2(testData["Subreddit"]);

            Validate(flairs);
        }

        [TestMethod]
        public void ClearFlairTemplates()
        {
            GenericContainer res1 = reddit.Models.Flair.ClearFlairTemplates("LINK_FLAIR", testData["Subreddit"]);
            GenericContainer res2 = reddit.Models.Flair.ClearFlairTemplates("USER_FLAIR", testData["Subreddit"]);

            Validate(res1);
            Validate(res2);
        }

        [TestMethod]
        public void DeleteFlair()
        {
            GenericContainer res = reddit.Models.Flair.DeleteFlair("KrisCraig", testData["Subreddit"]);

            Validate(res);
        }

        [TestMethod]
        public void Create()
        {
            GenericContainer res = reddit.Models.Flair.Create(new FlairCreateInput("Test User Flair", "", "KrisCraig"), testData["Subreddit"]);

            Validate(res);
        }

        [TestMethod]
        public void FlairConfig()
        {
            GenericContainer res = reddit.Models.Flair.FlairConfig(new FlairConfigInput(true, true, "right", true, "right"), testData["Subreddit"]);

            Validate(res);
        }

        [TestMethod]
        public void FlairCSV()
        {
            List<ActionResult> res = reddit.Models.Flair.FlairCSV("KrisCraig,Human," + Environment.NewLine + "RedditDotNetBot,Robot,", testData["Subreddit"]);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count == 2);
            foreach (ActionResult actionResult in res)
            {
                Assert.IsTrue(actionResult.Ok);
            }
        }

        [TestMethod]
        public void FlairList()
        {
            FlairListResultContainer res = reddit.Models.Flair.FlairList(new FlairNameListingInput(), testData["Subreddit"]);

            Validate(res);
        }

        [TestMethod]
        public void FlairSelector()
        {
            FlairSelectorResultContainer res = reddit.Models.Flair.FlairSelector(new FlairLinkInput(), testData["Subreddit"]);
            FlairSelectorResultContainer resUser = reddit.Models.Flair.FlairSelector(new FlairLinkInput(name: "KrisCraig"), testData["Subreddit"]);
            FlairSelectorResultContainer resLink = reddit.Models.Flair.FlairSelector(new FlairLinkInput("t3_9rirb3"), "RedditDotNETBot");

            Validate(res);
            Validate(resUser);
            Validate(resLink);
        }

        [TestMethod]
        public void LinkFlair()
        {
            List<Flair> flairs = reddit.Models.Flair.LinkFlair(testData["Subreddit"]);

            Validate(flairs);
        }

        [TestMethod]
        public void LinkFlairV2()
        {
            List<FlairV2> flairs = reddit.Models.Flair.LinkFlairV2(testData["Subreddit"]);

            Validate(flairs);
        }

        [TestMethod]
        public void SetFlairEnabled()
        {
            GenericContainer res = reddit.Models.Flair.SetFlairEnabled(true, testData["Subreddit"]);

            Validate(res);
        }

        [TestMethod]
        public void FlairTemplate()
        {
            GenericContainer resLink = reddit.Models.Flair.FlairTemplate(new FlairTemplateInput(DateTime.Now.ToString("fffffff"), "LINK_FLAIR", false), testData["Subreddit"]);
            GenericContainer resUser = reddit.Models.Flair.FlairTemplate(new FlairTemplateInput(DateTime.Now.ToString("fffffff"), "USER_FLAIR", false), testData["Subreddit"]);

            Validate(resLink);
            Validate(resUser);
        }
    }
}
