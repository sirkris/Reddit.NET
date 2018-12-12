using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Controllers;
using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class PrivateMessagesTests : BaseTests
    {
        private MessagesUpdateEventArgs E;

        public PrivateMessagesTests() : base() { }

        [TestMethod]
        public void Conversation()
        {
            User me = reddit.Account.Me;
            User patsy = GetTargetUser();

            string subject = "Test Message.";
            string body = "This is a test message.";
            string bodyReply = "Test message received and acknowledged.";

            reddit2.Account.Messages.MarkAllRead();

            reddit.Account.Messages.Compose(patsy.Name, subject, body);
            reddit2.Account.Messages.Compose(me.Name, subject, bodyReply);

            Assert.IsTrue(MonitorForMessage(reddit2.Account.Messages, me.Name, subject, body));
            Assert.IsTrue(MonitorForMessage(reddit.Account.Messages, patsy.Name, subject, bodyReply));

            reddit.Account.Messages.CollapseMessage(reddit.Account.Messages.Unread[0].Fullname);
            reddit.Account.Messages.UncollapseMessage(reddit.Account.Messages.Unread[0].Fullname);

            reddit2.Account.Messages.ReadMessage(reddit.Account.Messages.Unread[0].Fullname);
            reddit2.Account.Messages.UnreadMessage(reddit.Account.Messages.Unread[0].Fullname);

            reddit2.Account.Messages.DeleteMessage(reddit.Account.Messages.Unread[0].Fullname);
        }

        /// <summary>
        /// Monitor inbox for new unread messages.
        /// </summary>
        /// <param name="messages">PrivateMessages instance</param>
        /// <param name="from">Sender's username</param>
        /// <param name="subject">Message subject</param>
        /// <param name="body">Message body</param>
        /// <param name="timeoutMs">How long to wait for the message to arrive before giving up</param>
        /// <returns>Whether the requested message was found.</returns>
        private bool MonitorForMessage(PrivateMessages messages, string from, string subject, string body, int timeoutMs = 30000)
        {
            messages.MonitorUnread();
            messages.UnreadUpdated += C_UnreadMessagesUpdated;

            DateTime start = DateTime.Now;
            bool res = false;
            while (!res 
                && start.AddMilliseconds(timeoutMs) > DateTime.Now)
            {
                if (E != null)
                {
                    res = CheckMessages(E.NewMessages, from, subject, body);
                }
            }

            messages.UnreadUpdated -= C_UnreadMessagesUpdated;
            messages.MonitorUnread();

            return res;
        }

        private bool CheckMessages(List<RedditThings.Message> messages, string from, string subject, string body)
        {
            foreach (RedditThings.Message message in messages)
            {
                if (CheckMessage(message, from, subject, body))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckMessage(RedditThings.Message message, string from, string subject, string body)
        {
            return (message.Author.Equals(from)
                && message.Subject.Equals(subject)
                && message.Body.Equals(body));
        }

        private void C_UnreadMessagesUpdated(object sender, MessagesUpdateEventArgs e)
        {
            E = e;
        }
    }
}
