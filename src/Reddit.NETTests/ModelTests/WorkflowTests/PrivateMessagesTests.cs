using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Inputs.PrivateMessages;
using Reddit.Things;
using System;

namespace RedditTests.ModelTests.WorkflowTests
{
    [TestClass]
    public class PrivateMessagesTests : BaseTests
    {
        public PrivateMessagesTests() : base() { }

        private bool MessageExists(MessageContainer messages, string author, string subject, string body, out Message message)
        {
            Validate(messages);
            Validate(messages.Data);
            Validate(messages.Data.Children);

            message = null;
            foreach (MessageChild messageChild in messages.Data.Children)
            {
                if (messageChild.Data.Author.Equals(author)
                    && messageChild.Data.Subject.Equals(subject)
                    && messageChild.Data.Body.Equals(body))
                {
                    message = messageChild.Data;
                    return true;
                }
            }

            return false;
        }

        private bool MessageExists(string author, string subject, string body, out Message message, int waitMs = 15000)
        {
            DateTime start = DateTime.Now;
            bool res = false;
            do
            {
                res = MessageExists(reddit.Models.PrivateMessages.GetMessages("unread", new PrivateMessagesGetMessagesInput()), author, subject, body, out message);
            } while (!res && start.AddMilliseconds(waitMs) > DateTime.Now);

            return res;
        }

        [TestMethod]
        public void Conversation()
        {
            User me = reddit.Models.Account.Me();
            User patsy = GetTargetUserModel();

            string subject = "Test Message: " + DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
            string body = "This is a test message sent by Reddit.NET.";

            Validate(reddit2.Models.PrivateMessages.Compose(new PrivateMessagesComposeInput(subject: subject, text: body, to: me.Name)));

            // Note that this will fail if the primary test user is blocking the secondary test user.  You can confirm by running the corresponding controller test.  --Kris
            Assert.IsTrue(MessageExists(patsy.Name, subject, body, out Message message));

            reddit.Models.PrivateMessages.CollapseMessage(message.Name);
            reddit.Models.PrivateMessages.UncollapseMessage(message.Name);

            reddit.Models.PrivateMessages.ReadMessage(message.Name);
            reddit.Models.PrivateMessages.UnreadMessage(message.Name);
            reddit.Models.PrivateMessages.ReadAllMessages("");

            // Uncomment to test block feature.  The only way to reverse this is via the desktop site for some reason.  --Kris
            //reddit.Models.PrivateMessages.Block(message.Name);

            reddit.Models.PrivateMessages.DelMsg(message.Name);
        }
    }
}
