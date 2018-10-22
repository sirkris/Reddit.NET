using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

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

            Assert.IsNotNull(modQueue);
        }

        [TestMethod]
        public void ModQueueSpam()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer modQueue = reddit.Models.Moderation.ModQueue("spam", null, null, "comments", testData["Subreddit"]);

            Assert.IsNotNull(modQueue);
        }

        [TestMethod]
        public void ModQueue()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer modQueue = reddit.Models.Moderation.ModQueue("modqueue", null, null, "links", testData["Subreddit"]);

            Assert.IsNotNull(modQueue);
        }

        [TestMethod]
        public void ModQueueUnmoderated()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer modQueue = reddit.Models.Moderation.ModQueue("unmoderated", null, null, "links", testData["Subreddit"]);

            Assert.IsNotNull(modQueue);
        }

        [TestMethod]
        public void ModQueueEdited()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer modQueue = reddit.Models.Moderation.ModQueue("edited", null, null, "links", testData["Subreddit"]);

            Assert.IsNotNull(modQueue);
        }
    }
}
