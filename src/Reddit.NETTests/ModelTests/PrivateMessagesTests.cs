using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class PrivateMessagesTests : BaseTests
    {
        [TestMethod]
        public void GetMessagesInbox()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            MessageContainer messages = reddit.Models.PrivateMessages.GetMessages("inbox", false, "", "", "", false);

            Assert.IsNotNull(messages);
        }

        [TestMethod]
        public void GetMessagesUnread()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            MessageContainer messages = reddit.Models.PrivateMessages.GetMessages("unread", false, "", "", "", false);

            Assert.IsNotNull(messages);
        }

        [TestMethod]
        public void GetMessagesSent()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            MessageContainer messages = reddit.Models.PrivateMessages.GetMessages("sent", false, "", "", "", false);

            Assert.IsNotNull(messages);
        }

        [TestMethod]
        public void Compose()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            GenericContainer res = reddit.Models.PrivateMessages.Compose("", "", "Test Message", "This is a test.  So there.", "RedditDotNetBot");

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ComposeWithSr()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            GenericContainer res = reddit.Models.PrivateMessages.Compose(testData["Subreddit"], "", "Test Message", "This is a test.  So there.", "RedditDotNetBot");

            Assert.IsNotNull(res);
        }
    }
}
