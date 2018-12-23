using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET.Models.Structures;

namespace Reddit.NETTests.ModelTests
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
