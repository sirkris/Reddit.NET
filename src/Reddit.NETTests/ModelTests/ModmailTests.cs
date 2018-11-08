using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class ModmailTests : BaseTests
    {
        [TestMethod]
        public void Subreddits()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            ModmailSubredditContainer subs = reddit.Models.Modmail.Subreddits();

            Assert.IsNotNull(subs);
        }

        [TestMethod]
        public void UnreadCount()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            ModmailUnreadCount unreadCount = reddit.Models.Modmail.UnreadCount();

            Assert.IsNotNull(unreadCount);
        }
    }
}
