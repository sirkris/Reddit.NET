using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Models.Structures;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class PrivateMessagesTests : BaseTests
    {
        public PrivateMessagesTests() : base() { }

        [TestMethod]
        public void GetMessagesInbox()
        {
            MessageContainer messages = reddit.Models.PrivateMessages.GetMessages("inbox", false, "", "", "", false);

            Assert.IsNotNull(messages);
        }

        [TestMethod]
        public void GetMessagesUnread()
        {
            MessageContainer messages = reddit.Models.PrivateMessages.GetMessages("unread", false, "", "", "", false);

            Assert.IsNotNull(messages);
        }

        [TestMethod]
        public void GetMessagesSent()
        {
            MessageContainer messages = reddit.Models.PrivateMessages.GetMessages("sent", false, "", "", "", false);

            Assert.IsNotNull(messages);
        }

        [TestMethod]
        public void Compose()
        {
            GenericContainer res = reddit.Models.PrivateMessages.Compose("", "", "Test Message", "This is a test.  So there.", "RedditDotNetBot");

            Assert.IsNotNull(res);
        }

        // Requires existing test subreddit.  --Kris
        [TestMethod]
        public void ComposeWithSr()
        {
            GenericContainer res = reddit.Models.PrivateMessages.Compose(testData["Subreddit"], "", "Test Message", "This is a test.  So there.", "RedditDotNetBot");

            Assert.IsNotNull(res);
        }
    }
}
