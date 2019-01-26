using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RedditTests.ControllerTests
{
    [TestClass]
    public class PrivateMessagesTests : BaseTests
    {
        public PrivateMessagesTests() : base() { }

        [TestMethod]
        public void Inbox()
        {
            Validate(reddit.Account.Messages.Inbox);
        }

        [TestMethod]
        public void Unread()
        {
            Validate(reddit.Account.Messages.Unread);
        }

        [TestMethod]
        public void Sent()
        {
            Validate(reddit.Account.Messages.Sent);
        }
    }
}
