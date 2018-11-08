using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using RestSharp;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class ModerationTests : BaseTests
    {
        [TestMethod]
        public void GetLog()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            ModActionContainer log = reddit.Models.Moderation.GetLog(null, null, testData["Subreddit"]);

            Assert.IsNotNull(log);
        }

        [TestMethod]
        public void ModQueueReports()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer modQueue = reddit.Models.Moderation.ModQueue("reports", null, null, "links", testData["Subreddit"]);

            Validate(modQueue, true);
        }

        [TestMethod]
        public void ModQueueSpam()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer modQueue = reddit.Models.Moderation.ModQueue("spam", null, null, "comments", testData["Subreddit"]);

            Validate(modQueue, true);
        }

        [TestMethod]
        public void ModQueue()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer modQueue = reddit.Models.Moderation.ModQueue("modqueue", null, null, "links", testData["Subreddit"]);

            Validate(modQueue, true);
        }

        [TestMethod]
        public void ModQueueUnmoderated()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer modQueue = reddit.Models.Moderation.ModQueue("unmoderated", null, null, "links", testData["Subreddit"]);

            Validate(modQueue, true);
        }

        [TestMethod]
        public void ModQueueEdited()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer modQueue = reddit.Models.Moderation.ModQueue("edited", null, null, "links", testData["Subreddit"]);

            Validate(modQueue, true);
        }

        [TestMethod]
        public void Stylesheet()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

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

        // Requires existing subreddit with mod privilages.  --Kris
        // TODO - Move this to controller tests since it hits non-moderation endpoints.  --Kris
        [TestMethod]
        public void Approve()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            Post post = reddit.Models.Listings.New(null, null, true, testData["Subreddit"]).Data.Children[0].Data;

            reddit.Models.Moderation.Approve(post.Name);

            post = reddit.Models.LinksAndComments.Info(post.Name).Posts[0];

            Assert.IsNotNull(post);
            Assert.IsTrue(post.Approved);
        }
    }
}
