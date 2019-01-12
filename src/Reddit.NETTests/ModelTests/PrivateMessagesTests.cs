using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Inputs.PrivateMessages;
using Reddit.Things;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class PrivateMessagesTests : BaseTests
    {
        public PrivateMessagesTests() : base() { }

        [TestMethod]
        public void GetMessagesInbox()
        {
            MessageContainer messages = reddit.Models.PrivateMessages.GetMessages("inbox", new PrivateMessagesGetMessagesInput());

            Assert.IsNotNull(messages);
        }

        [TestMethod]
        public void GetMessagesUnread()
        {
            MessageContainer messages = reddit.Models.PrivateMessages.GetMessages("unread", new PrivateMessagesGetMessagesInput());

            Assert.IsNotNull(messages);
        }

        [TestMethod]
        public void GetMessagesSent()
        {
            MessageContainer messages = reddit.Models.PrivateMessages.GetMessages("sent", new PrivateMessagesGetMessagesInput());

            Assert.IsNotNull(messages);
        }

        [TestMethod]
        public void Compose()
        {
            GenericContainer res = reddit.Models.PrivateMessages.Compose(new PrivateMessagesComposeInput(subject: "Test Message", text: "This is a test.  So there.", to: "RedditDotNetBot"));

            Assert.IsNotNull(res);
        }

        // Requires existing test subreddit.  --Kris
        [TestMethod]
        public void ComposeWithSr()
        {
            GenericContainer res = reddit.Models.PrivateMessages.Compose(new PrivateMessagesComposeInput(testData["Subreddit"], "Test Message", "This is a test.  So there.", "RedditDotNetBot"));

            Assert.IsNotNull(res);
        }
    }
}
