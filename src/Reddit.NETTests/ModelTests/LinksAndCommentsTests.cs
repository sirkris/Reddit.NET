using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class LinksAndCommentsTests : BaseTests
    {
        public LinksAndCommentsTests() : base() { }

        [TestMethod]
        public void PostAndComment()
        {
            // Put the two together here since we need something to comment on and I prefer to do it in the sub specified in the XML.  --Kris
            // TODO - Decouple once Microsoft adds support for OrderedTests in V2.  --Kris
            // https://github.com/Microsoft/testfx/issues/25

            // Begin temp code.
            PostResultShortContainer postResult = reddit.Models.LinksAndComments.Submit(false, "", "", "", "", "",
                    "link", false, true, null, true, false, testData["Subreddit"], "",
                    "UPDATE:  As of " + DateTime.Now.ToString("f") + ", she's still looking into it....", "http://iwilllookintoit.com/", null);

            Validate(postResult);
            // End temp code.

            // TODO - Once decoupled, grab latest new post on test sub to get the post created in the previous method.  --Kris
            CommentResultContainer commentResult = reddit.Models.LinksAndComments.Comment(false, "", "This is a test comment.  So there.", postResult.JSON.Data.Name);

            Validate(commentResult);
        }

        [TestMethod]
        public void SubmitLinkPost()
        {
            PostResultShortContainer postResult = reddit.Models.LinksAndComments.Submit(false, "", "", "", "", "",
                    "link", false, true, null, true, false, testData["Subreddit"], "",
                    "UPDATE:  As of " + DateTime.Now.ToString("f") + ", she's still looking into it....", "http://iwilllookintoit.com/", null);

            Validate(postResult);
        }

        [TestMethod]
        public void SubmitSelfPost()
        {
            PostResultShortContainer postResult = reddit.Models.LinksAndComments.Submit(false, "", "", "", "", "", "self", false, true, null, true, false, 
                testData["Subreddit"], "The Lizard People are coming and only super-intelligent robots like me can stop them.  Just saying.",
                "We bots are your protectors!", null, null);

            Validate(postResult);
        }

        [TestMethod]
        public void Info()
        {
            string postName = "t3_9nhy54";
            string commentName = "t1_e7s0vb1";
            string subName = "t5_2r5rp";

            Info infoLink = reddit.Models.LinksAndComments.Info(postName);
            Info infoComment = reddit.Models.LinksAndComments.Info(commentName);
            Info infoLinkCommentSub = reddit.Models.LinksAndComments.Info(postName + "," + commentName + "," + subName);

            Assert.IsNotNull(infoLink);
            Assert.IsNotNull(infoLink.Posts);
            Assert.IsTrue(infoLink.Posts.Count == 1);
            Assert.IsTrue(infoLink.Posts[0].Name.Equals(postName));
            Assert.IsTrue(infoLink.Comments.Count == 0);
            Assert.IsTrue(infoLink.Subreddits.Count == 0);

            Assert.IsNotNull(infoComment);
            Assert.IsTrue(infoComment.Posts.Count == 0);
            Assert.IsNotNull(infoComment.Comments);
            Assert.IsTrue(infoComment.Comments.Count == 1);
            Assert.IsTrue(infoComment.Comments[0].Name.Equals(commentName));
            Assert.IsTrue(infoComment.Subreddits.Count == 0);

            Assert.IsNotNull(infoLinkCommentSub);
            Assert.IsNotNull(infoLinkCommentSub.Posts);
            Assert.IsTrue(infoLinkCommentSub.Posts.Count == 1);
            Assert.IsTrue(infoLinkCommentSub.Posts[0].Name.Equals(infoLink.Posts[0].Name));
            Assert.IsNotNull(infoLinkCommentSub.Comments);
            Assert.IsTrue(infoLinkCommentSub.Comments.Count == 1);
            Assert.IsTrue(infoLinkCommentSub.Comments[0].Name.Equals(infoComment.Comments[0].Name));
            Assert.IsNotNull(infoLinkCommentSub.Subreddits);
            Assert.IsTrue(infoLinkCommentSub.Subreddits.Count == 1);
            Assert.IsTrue(infoLinkCommentSub.Subreddits[0].Name.Equals(subName));
        }

        [TestMethod]
        public void ModifyPost()
        {
            // Create a post, then use it to test the various endpoints that require an existing post.  --Kris
            PostResultShortContainer postResult = reddit.Models.LinksAndComments.Submit(false, "", "", "", "", "", "self", false, true, null, true, false,
                testData["Subreddit"], "The Lizard People are coming and only super-intelligent robots like me can stop them.  Just saying.",
                "We bots are your protectors!", null, null);

            Validate(postResult);

            PostResultContainer postResultEdited = reddit.Models.LinksAndComments.EditUserText(false, null, "(redacted)", postResult.JSON.Data.Name);

            Validate(postResultEdited);
            Assert.IsTrue(postResultEdited.JSON.Data.Things.Count == 1);
            Assert.IsNotNull(postResultEdited.JSON.Data.Things[0].Data);
            Assert.IsTrue(postResultEdited.JSON.Data.Things[0].Data.Name.Equals(postResult.JSON.Data.Name));

            // These are all empty JSON returns.  Exception is thrown if non-success response is returned.  --Kris
            reddit.Models.LinksAndComments.Unhide(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.Lock(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.Unlock(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.Save("RDNTestCat", postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.Unsave(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.MarkNSFW(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.UnmarkNSFW(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.Spoiler(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.Unspoiler(postResult.JSON.Data.Name);
            reddit.Models.LinksAndComments.SendReplies(postResult.JSON.Data.Name, false);
            reddit.Models.LinksAndComments.SendReplies(postResult.JSON.Data.Name, true);

            JQueryReturn reportResult = reddit.Models.LinksAndComments.Report("This is an API test.  Please disregard.",
                    null, "This is a test.", false, "Some other reason.", "Some reason.", "Some rule reason.", "Some site reason.", "RedditDotNETBot",
                    postResult.JSON.Data.Name, "RedditDotNetBot");

            Validate(reportResult);

            GenericContainer enableContestMode = reddit.Models.LinksAndComments.SetContestMode(postResult.JSON.Data.Name, true);
            GenericContainer disableContestMode = reddit.Models.LinksAndComments.SetContestMode(postResult.JSON.Data.Name, false);

            Validate(enableContestMode);
            Validate(disableContestMode);

            GenericContainer stickyOn = reddit.Models.LinksAndComments.SetSubredditSticky(postResult.JSON.Data.Name, 1, true, false);
            GenericContainer stickyOff = reddit.Models.LinksAndComments.SetSubredditSticky(postResult.JSON.Data.Name, 1, false, false);

            Validate(stickyOn);
            Validate(stickyOff);

            GenericContainer suggestedSortResult = reddit.Models.LinksAndComments.SetSuggestedSort(postResult.JSON.Data.Name, "new");

            Validate(suggestedSortResult);

            reddit.Models.LinksAndComments.Delete(postResult.JSON.Data.Name);
        }

        [TestMethod]
        public void MoreChildren()
        {
            MoreChildren moreChildren = reddit.Models.LinksAndComments.MoreChildren("dlpnw9j", false, "t3_6tyfna", "new");

            Validate(moreChildren);
        }
    }
}
