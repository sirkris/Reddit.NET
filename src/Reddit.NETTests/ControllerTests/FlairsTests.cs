using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;
using Reddit.Inputs.Flair;
using System;

namespace RedditTests.ControllerTests
{
    [TestClass]
    public class FlairsTests : BaseTests
    {
        private Subreddit Subreddit
        {
            get
            {
                return subreddit ?? GetSubreddit(ref subreddit);
            }
            set
            {
                subreddit = value;
            }
        }
        private Subreddit subreddit;

        public FlairsTests() : base() { }

        [TestMethod]
        public void ClearLinkFlairTemplates()
        {
            Subreddit.Flairs.ClearLinkFlairTemplates();
        }

        [TestMethod]
        public void ClearUserFlairTemplates()
        {
            Subreddit.Flairs.ClearUserFlairTemplates();
        }

        [TestMethod]
        public void DeleteFlair()
        {
            Subreddit.Flairs.DeleteFlair("KrisCraig");
        }

        [TestMethod]
        public void FlairList()
        {
            Validate(Subreddit.Flairs.FlairList);
        }

        [TestMethod]
        public void LinkFlair()
        {
            Validate(Subreddit.Flairs.LinkFlair);
        }

        [TestMethod]
        public void LinkFlairV2()
        {
            Validate(Subreddit.Flairs.LinkFlairV2);
        }

        [TestMethod]
        public void UserFlair()
        {
            Validate(Subreddit.Flairs.UserFlair);
        }

        [TestMethod]
        public void UserFlairV2()
        {
            Validate(Subreddit.Flairs.UserFlairV2);
        }

        [TestMethod]
        public void CreateUserFlair()
        {
            Subreddit.Flairs.CreateUserFlair("KrisCraig", "Human");
        }

        [TestMethod]
        public void FlairConfig()
        {
            Subreddit.Flairs.FlairConfig(new FlairConfigInput(true, true, "right", true, "right"));
        }

        [TestMethod]
        public void FlairCSV()
        {
            Validate(Subreddit.Flairs.FlairCSV("KrisCraig,Human," + Environment.NewLine + "RedditDotNetBot,Robot,"), 2);
        }

        [TestMethod]
        public void FlairSelector()
        {
            Validate(Subreddit.Flairs.FlairSelector("KrisCraig"));
        }

        [TestMethod]
        public void CreateLinkFlairTemplate()
        {
            Subreddit.Flairs.CreateLinkFlairTemplate(DateTime.Now.ToString("fffffff"));
        }

        [TestMethod]
        public void CreateUserFlairTemplate()
        {
            Subreddit.Flairs.CreateUserFlairTemplate(DateTime.Now.ToString("fffffff"));
        }

        [TestMethod]
        public void CreateLinkFlairTemplateV2()
        {
            Subreddit.Flairs.CreateLinkFlairTemplateV2("V2-" + DateTime.Now.ToString("fffffff"));
        }

        [TestMethod]
        public void CreateUserFlairTemplateV2()
        {
            Subreddit.Flairs.CreateUserFlairTemplateV2("V2-" + DateTime.Now.ToString("fffffff"));
        }

        [TestMethod]
        public void SetFlairEnabled()
        {
            Subreddit.Flairs.SetFlairEnabled();
        }
    }
}
