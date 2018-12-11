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
    public class ModmailTests : BaseTests
    {
        private Dictionary<string, string> NewMessages;

        public ModmailTests() : base()
        {
            NewMessages = new Dictionary<string, string>();
        }

        [TestMethod]
        public void GetConversations()
        {
            Validate(reddit.Account.Modmail.GetConversations());
            Validate(reddit.Account.Modmail.GetConversations(entity: testData["Subreddit"]));
        }

        [TestMethod]
        public void Conversation()
        {
            Validate(reddit2.Account.Modmail.NewConversation("This is a controller test.", "Test Message", testData["Subreddit"]));

            // If user is already muted, we can't continue because Unmute requires the ID of the conversation in which the user was originally muted.  --Kris
            RedditThings.ModmailConversationContainer modmailConversationContainer = null;
            try
            {
                modmailConversationContainer = reddit2.Account.Modmail.NewConversation("This is a controller test.", "Test Message", testData["Subreddit"]);
            }
            catch (RedditBadRequestException ex)
            {
                CheckBadRequest("USER_MUTED", "Target user cannot be muted when the test begins.", ex);
            }

            Validate(modmailConversationContainer);
            Assert.AreEqual(1, modmailConversationContainer.Messages.Count);

            RedditThings.ModmailConversationContainer modmailConversationContainer2 = reddit.Account.Modmail.GetConversation(modmailConversationContainer.Conversation.Id);

            Validate(modmailConversationContainer2);
            Assert.AreEqual(modmailConversationContainer.Conversation.Id, modmailConversationContainer2.Conversation.Id);

            modmailConversationContainer2 = reddit.Account.Modmail.NewMessage(modmailConversationContainer2.Conversation.Id, "This is a test reply.");

            Validate(modmailConversationContainer2);
            Assert.AreEqual(2, modmailConversationContainer2.Messages.Count);
            Assert.AreEqual(modmailConversationContainer.Conversation.Id, modmailConversationContainer2.Conversation.Id);

            modmailConversationContainer2 = reddit.Account.Modmail.MarkHighlighted(modmailConversationContainer2.Conversation.Id);

            Validate(modmailConversationContainer2);
            Assert.IsTrue(modmailConversationContainer2.Conversation.IsHighlighted);
            Assert.AreEqual(modmailConversationContainer.Conversation.Id, modmailConversationContainer2.Conversation.Id);

            modmailConversationContainer2 = reddit.Account.Modmail.RemoveHighlight(modmailConversationContainer2.Conversation.Id);

            Validate(modmailConversationContainer2);
            Assert.IsFalse(modmailConversationContainer2.Conversation.IsHighlighted);
            Assert.AreEqual(modmailConversationContainer.Conversation.Id, modmailConversationContainer2.Conversation.Id);

            Validate(reddit.Account.Modmail.Mute(modmailConversationContainer2.Conversation.Id));
            Validate(reddit.Account.Modmail.Unmute(modmailConversationContainer2.Conversation.Id));

            Validate(reddit.Account.Modmail.GetUserConversations(modmailConversationContainer2.Conversation.Id));

            reddit.Account.Modmail.MarkRead(modmailConversationContainer2.Conversation.Id);
            reddit.Account.Modmail.MarkUnread(modmailConversationContainer2.Conversation.Id);
        }

        [TestMethod]
        public void MonitorUnreadMessages()
        {
            User patsy = GetTargetUser();

            RedditThings.ModmailConversationContainer conversation = reddit2.Account.Modmail.NewConversation("This is a new modmail conversation.", "Test Message", testData["Subreddit"]);

            for (int i = 1; i <= 10; i++)
            {
                // Despite what VS says, we don't want to use await here.  --Kris
                reddit2.Account.Modmail.NewMessageAsync(conversation.Conversation.Id, "Test message " + i.ToString());
            }

            reddit.Account.Modmail.MonitorUnread();
            reddit.Account.Modmail.UnreadUpdated += C_UnreadMessagesUpdated;

            DateTime start = DateTime.Now;
            while (NewMessages.Count < 10
                && start.AddSeconds(30) > DateTime.Now) { }

            reddit.Account.Modmail.UnreadUpdated -= C_UnreadMessagesUpdated;
            reddit.Account.Modmail.MonitorUnread();

            Assert.IsTrue(NewMessages.Count >= 10);
        }

        private void C_UnreadMessagesUpdated(object sender, ModmailConversationsEventArgs e)
        {
            foreach (KeyValuePair<string, RedditThings.ConversationMessage> pair in e.AddedMessages)
            {
                if (!NewMessages.ContainsKey(pair.Value.BodyMarkdown))
                {
                    NewMessages.Add(pair.Value.BodyMarkdown, pair.Value.Author.Name);
                }
            }
        }
    }
}
