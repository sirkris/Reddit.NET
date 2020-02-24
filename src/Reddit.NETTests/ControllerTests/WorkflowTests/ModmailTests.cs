using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using Reddit.Exceptions;
using System;
using System.Collections.Generic;

namespace RedditTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class ModmailTests : BaseTests
    {
        private int NewMessages;

        public ModmailTests() : base()
        {
            NewMessages = 0;
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
            Reddit.Things.ModmailConversationContainer modmailConversationContainer = null;
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

            // TODO - See:  https://www.reddit.com/r/redditdev/comments/exofpt/new_issue_api_now_returns_403_on_post/fgnu5n2/?context=4
            // When compose is fixed to return an ID so we can identify the new message we just created, remove assert and update returns to accommodate the new JSON.  --Kris
            Assert.Inconclusive("This test cannot proceed due to a bug in the Reddit API.  See:  https://www.reddit.com/r/redditdev/comments/exofpt/new_issue_api_now_returns_403_on_post/fgnu5n2/?context=4");

            Reddit.Things.ModmailConversationContainer modmailConversationContainer2 = reddit.Account.Modmail.GetConversation(modmailConversationContainer.Conversation.Id);

            Validate(modmailConversationContainer2);
            Assert.AreEqual(modmailConversationContainer.Conversation.Id, modmailConversationContainer2.Conversation.Id);

            modmailConversationContainer2 = reddit.Account.Modmail.NewMessage(modmailConversationContainer2.Conversation.Id, "This is a test reply.");

            Validate(modmailConversationContainer2);
            Assert.IsTrue(modmailConversationContainer2.Messages.Count >= 2);
            Assert.AreEqual(modmailConversationContainer.Conversation.Id, modmailConversationContainer2.Conversation.Id);

            modmailConversationContainer2 = reddit.Account.Modmail.MarkHighlighted(modmailConversationContainer2.Conversation.Id);

            Validate(modmailConversationContainer2);
            Assert.IsTrue(modmailConversationContainer2.Conversation.IsHighlighted);
            Assert.AreEqual(modmailConversationContainer.Conversation.Id, modmailConversationContainer2.Conversation.Id);

            modmailConversationContainer2 = reddit.Account.Modmail.RemoveHighlight(modmailConversationContainer2.Conversation.Id);

            Validate(modmailConversationContainer2);
            Assert.IsFalse(modmailConversationContainer2.Conversation.IsHighlighted);
            Assert.AreEqual(modmailConversationContainer.Conversation.Id, modmailConversationContainer2.Conversation.Id);

            // TODO - If target user is a moderator, revoke their mod status before this test begins.  --Kris
            try
            {
                Validate(reddit.Account.Modmail.Mute(modmailConversationContainer2.Conversation.Id));
                Validate(reddit.Account.Modmail.Unmute(modmailConversationContainer2.Conversation.Id));
            }
            catch (RedditBadRequestException ex)
            {
                try { CheckBadRequest("CANT_RESTRICT_MODERATOR", "Moderators can't be muted.", ex); } catch (AssertInconclusiveException) { }
            }

            Validate(reddit.Account.Modmail.GetUserConversations(modmailConversationContainer2.Conversation.Id));

            reddit.Account.Modmail.MarkRead(modmailConversationContainer2.Conversation.Id);
            reddit.Account.Modmail.MarkUnread(modmailConversationContainer2.Conversation.Id);
        }

        [TestMethod]
        public void MonitorUnreadMessages()
        {
            reddit.Account.Modmail.GetUnreadConversations();

            User patsy = GetTargetUser();

            Reddit.Things.ModmailConversationContainer conversation = reddit2.Account.Modmail.NewConversation("This is a new modmail conversation.", "Test Message", testData["Subreddit"]);

            // TODO - See:  https://www.reddit.com/r/redditdev/comments/exofpt/new_issue_api_now_returns_403_on_post/fgnu5n2/?context=4
            // When compose is fixed to return an ID so we can identify the new message we just created, remove assert and update returns to accommodate the new JSON.  --Kris
            Assert.Inconclusive("This test cannot proceed due to a bug in the Reddit API.  See:  https://www.reddit.com/r/redditdev/comments/exofpt/new_issue_api_now_returns_403_on_post/fgnu5n2/?context=4");

            for (int i = 1; i <= 10; i++)
            {
                reddit2.Account.Modmail.NewMessage(conversation.Conversation.Id, "Test message " + i.ToString());
            }

            reddit.Account.Modmail.MonitorUnread();
            reddit.Account.Modmail.UnreadUpdated += C_UnreadMessagesUpdated;

            DateTime start = DateTime.Now;
            while (NewMessages < 10
                && start.AddSeconds(60) > DateTime.Now) { }

            reddit.Account.Modmail.UnreadUpdated -= C_UnreadMessagesUpdated;
            reddit.Account.Modmail.MonitorUnread();

            Assert.IsTrue(NewMessages >= 10);
        }

        private void C_UnreadMessagesUpdated(object sender, ModmailConversationsEventArgs e)
        {
            foreach (KeyValuePair<string, Reddit.Things.ConversationMessage> pair in e.AddedMessages)
            {
                NewMessages++;
            }
        }
    }
}
