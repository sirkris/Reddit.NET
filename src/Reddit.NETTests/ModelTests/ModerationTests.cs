using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Inputs.Moderation;
using Reddit.Things;
using RestSharp;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class ModerationTests : BaseTests
    {
        public ModerationTests() : base() { }

        [TestMethod]
        public void GetLog()
        {
            ModActionContainer log = reddit.Models.Moderation.GetLog(new ModerationGetLogInput(), testData["Subreddit"]);

            Assert.IsNotNull(log);
        }

        [TestMethod]
        public void ModQueueReports()
        {
            PostContainer modQueue = reddit.Models.Moderation.ModQueue(new ModerationModQueueInput(), "reports", testData["Subreddit"]);

            Validate(modQueue, true);
        }

        [TestMethod]
        public void ModQueueSpam()
        {
            PostContainer modQueue = reddit.Models.Moderation.ModQueue(new ModerationModQueueInput("comments"), "spam", testData["Subreddit"]);

            Validate(modQueue, true);
        }

        [TestMethod]
        public void ModQueue()
        {
            PostContainer modQueue = reddit.Models.Moderation.ModQueue(new ModerationModQueueInput(), "modqueue", testData["Subreddit"]);

            Validate(modQueue, true);
        }

        [TestMethod]
        public void ModQueueUnmoderated()
        {
            PostContainer modQueue = reddit.Models.Moderation.ModQueue(new ModerationModQueueInput(), "unmoderated", testData["Subreddit"]);

            Validate(modQueue, true);
        }

        [TestMethod]
        public void ModQueueEdited()
        {
            PostContainer modQueue = reddit.Models.Moderation.ModQueue(new ModerationModQueueInput(), "edited", testData["Subreddit"]);

            Validate(modQueue, true);
        }

        [TestMethod]
        public void Stylesheet()
        {
            string css = "";
            try
            {
                css = reddit.Models.Moderation.Stylesheet(testData["Subreddit"]);
            }
            catch (System.Net.WebException ex)
            {
                if (!ex.Data.Contains("res")
                    || ((IRestResponse)ex.Data["res"]).StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    throw ex;
                }
                else
                {
                    Assert.Inconclusive("Subreddit does not contain a stylesheet.  Please create one and retest.");
                }
            }

            Assert.IsNotNull(css);
        }
    }
}
