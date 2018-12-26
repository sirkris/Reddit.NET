using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Things;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class ModmailTests : BaseTests
    {
        public ModmailTests() : base() { }

        [TestMethod]
        public void Subreddits()
        {
            ModmailSubredditContainer subs = reddit.Models.Modmail.Subreddits();

            Assert.IsNotNull(subs);
        }

        [TestMethod]
        public void UnreadCount()
        {
            ModmailUnreadCount unreadCount = reddit.Models.Modmail.UnreadCount();

            Assert.IsNotNull(unreadCount);
        }
    }
}
