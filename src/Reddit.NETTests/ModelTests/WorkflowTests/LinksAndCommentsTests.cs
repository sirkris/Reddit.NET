using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Inputs.LinksAndComments;
using Reddit.Inputs.PrivateMessages;
using Reddit.Things;
using System;
using System.Threading;

namespace RedditTests.ModelTests.WorkflowTests
{
    [TestClass]
    public class LinksAndCommentsTests : BaseTests
    {
        public LinksAndCommentsTests() : base() { }

        [TestMethod]
        public void PostAndComment()
        {
            PostResultShortContainer postResult = TestPost();

            Validate(postResult);

            CommentResultContainer commentResult = TestComment(postResult.JSON.Data.Name);

            Validate(commentResult);

            CommentResultContainer commentResult2 = TestCommentReply(commentResult.JSON.Data.Things[0].Data.Name);

            Validate(commentResult2);
        }

        [TestMethod]
        public void MessageReply()
        {
            User me = reddit.Models.Account.Me();
            User patsy = GetTargetUserModel();

            // Create the initial message.  --Kris
            reddit.Models.PrivateMessages.Compose(new PrivateMessagesComposeInput(subject: "Test Message", text: "This is a test.  So there.", to: patsy.Name));

            // Wait until the message arrives, then grab it.  The message ID is not returned by the Compose endpoint.  --Kris
            DateTime start = DateTime.Now;
            MessageContainer messages = null;
            while (start.AddMinutes(2) >= DateTime.Now
                && GetTestMessage(out messages, me.Name) == null)
            {
                Thread.Sleep(1500);
            }

            Message message = GetTestMessage(messages, me.Name);

            Assert.IsNotNull(message);  // If this fails, it likely means that the test message has not yet arrived or one test user is blocking the other.  --Kris

            // Now that we have our initial test message, let's reply to it.  --Kris
            CommentResultContainer commentResultContainer = reddit2.Models.LinksAndComments.Comment<CommentResultContainer>(new LinksAndCommentsThingInput("Message received.", message.Name));

            Validate(commentResultContainer);
        }

        private Message GetTestMessage(out MessageContainer messages, string sender, string subject = "Test Message")
        {
            messages = reddit2.Models.PrivateMessages.GetMessages("unread", new PrivateMessagesGetMessagesInput(true));

            return GetTestMessage(messages, sender, subject);
        }

        private Message GetTestMessage(MessageContainer messages, string sender, string subject = "Test Message")
        {
            foreach (MessageChild messageChild in messages.Data.Children)
            {
                if (messageChild != null
                    && messageChild.Data != null
                    && messageChild.Data.Author != null
                    && messageChild.Data.Author.Equals(sender)
                    && messageChild.Data.Subject.Equals(subject))
                {
                    return messageChild.Data;
                }
            }

            return null;
        }

        [TestMethod]
        public void ModifyPost()
        {
            // Create a post, then use it to test the various endpoints that require an existing post.  --Kris
            PostResultShortContainer postResult = reddit.Models.LinksAndComments.Submit(new LinksAndCommentsSubmitInput(false, "", "", "", "", "self", false, true, null, true, false,
                testData["Subreddit"], "The Lizard People are coming and only super-intelligent robots like me can stop them.  Just saying.",
                "We bots are your protectors!", null, null));

            Validate(postResult);

            PostResultContainer postResultEdited = reddit.Models.LinksAndComments.EditUserText(new LinksAndCommentsThingInput("(redacted)", postResult.JSON.Data.Name));

            Validate(postResultEdited);
            Assert.IsTrue(postResultEdited.JSON.Data.Things.Count == 1);
            Assert.IsNotNull(postResultEdited.JSON.Data.Things[0].Data);
            Assert.IsTrue(postResultEdited.JSON.Data.Things[0].Data.Name.Equals(postResult.JSON.Data.Name));

            // These are all empty JSON returns.  Exception is thrown if non-success response is returned.  --Kris
            reddit.Models.LinksAndComments.Unhide(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.Lock(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.Unlock(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.Save(new LinksAndCommentsSaveInput(postResult.JSON.Data.Name, "RDNTestCat"));
            reddit.Models.LinksAndComments.Unsave(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.MarkNSFW(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.UnmarkNSFW(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.Spoiler(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.Unspoiler(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.SendReplies(new LinksAndCommentsStateInput(postResult.JSON.Data.Name, false));
            reddit.Models.LinksAndComments.SendReplies(new LinksAndCommentsStateInput(postResult.JSON.Data.Name, true));

            JQueryReturn reportResult = reddit.Models.LinksAndComments.Report(new LinksAndCommentsReportInput("This is an API test.  Please disregard.",
                    null, "This is a test.", false, "Some other reason.", "Some reason.", "Some rule reason.", "Some site reason.", "RedditDotNETBot",
                    postResult.JSON.Data.Name, "RedditDotNetBot"));

            Validate(reportResult);

            GenericContainer enableContestMode = reddit.Models.LinksAndComments.SetContestMode(new LinksAndCommentsStateInput(postResult.JSON.Data.Name, true));
            GenericContainer disableContestMode = reddit.Models.LinksAndComments.SetContestMode(new LinksAndCommentsStateInput(postResult.JSON.Data.Name, false));

            Validate(enableContestMode);
            Validate(disableContestMode);

            GenericContainer stickyOn = reddit.Models.LinksAndComments.SetSubredditSticky(new LinksAndCommentsStickyInput(postResult.JSON.Data.Name, 1, true, false));
            GenericContainer stickyOff = reddit.Models.LinksAndComments.SetSubredditSticky(new LinksAndCommentsStickyInput(postResult.JSON.Data.Name, 1, false, false));

            Validate(stickyOn);
            Validate(stickyOff);

            GenericContainer suggestedSortResult = reddit.Models.LinksAndComments.SetSuggestedSort(new LinksAndCommentsSuggestedSortInput(postResult.JSON.Data.Name, "new"));

            Validate(suggestedSortResult);

            reddit.Models.LinksAndComments.Delete(postResult.JSON.Data.Name);
        }
    }
}
