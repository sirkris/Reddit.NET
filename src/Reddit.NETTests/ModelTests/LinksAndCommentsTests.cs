using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Inputs.LinksAndComments;
using Reddit.Things;
using System;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class LinksAndCommentsTests : BaseTests
    {
        public LinksAndCommentsTests() : base() { }

        // TODO - Create unit test for Comment method once Microsoft adds support for OrderedTests in V2.  --Kris
        // https://github.com/Microsoft/testfx/issues/25

        [TestMethod]
        public void SubmitLinkPost()
        {
            PostResultShortContainer postResult = reddit.Models.LinksAndComments.Submit(new LinksAndCommentsSubmitInput(false, "", "", "", "",
                    "link", false, true, null, true, false, testData["Subreddit"], "",
                    "UPDATE:  As of " + DateTime.Now.ToString("f") + ", she's still looking into it....", "http://iwilllookintoit.com/", null));

            Validate(postResult);
        }

        [TestMethod]
        public void SubmitSelfPost()
        {
            PostResultShortContainer postResult = reddit.Models.LinksAndComments.Submit(new LinksAndCommentsSubmitInput(false, "", "", "", "", "self", false, true, null, true, false,
                testData["Subreddit"], "The Lizard People are coming and only super-intelligent robots like me can stop them.  Just saying.",
                "We bots are your protectors!", null, null));

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
        public void MoreChildren()
        {
            MoreChildren moreChildren = reddit.Models.LinksAndComments.MoreChildren(new LinksAndCommentsMoreChildrenInput("dlpnw9j", false, "t3_6tyfna", "new"));

            Validate(moreChildren);
        }
    }
}
