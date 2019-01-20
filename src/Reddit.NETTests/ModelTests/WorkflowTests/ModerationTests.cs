using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Exceptions;
using Reddit.Inputs;
using Reddit.Inputs.Moderation;
using Reddit.Inputs.Users;
using Reddit.Things;

namespace RedditTests.ModelTests.WorkflowTests
{
    [TestClass]
    public class ModerationTests : BaseTests
    {
        public ModerationTests() : base() { }

        // Requires existing subreddit with mod privilages.  --Kris
        [TestMethod]
        public void Approve()
        {
            Post post = reddit.Models.Listings.New(new CategorizedSrListingInput(includeCategories: true), testData["Subreddit"]).Data.Children[0].Data;

            reddit.Models.Moderation.Approve(post.Name);

            post = reddit.Models.LinksAndComments.Info(post.Name).Posts[0];

            Assert.IsNotNull(post);
            Assert.IsTrue(post.Approved);
        }

        [TestMethod]
        public void ModeratorInvite()
        {
            User patsy = GetTargetUserModel();

            Validate(patsy);

            GenericContainer res = null;
            try
            {
                res = reddit.Models.Users.Friend(new UsersFriendInput(patsy.Name, "moderator_invite"), testData["Subreddit"]);

                Validate(res);

                res = reddit2.Models.Moderation.AcceptModeratorInvite(testData["Subreddit"]);

                Validate(res);
            }
            catch (AssertFailedException ex)
            {
                if (res == null
                    || res.JSON == null
                    || res.JSON.Errors == null
                    || res.JSON.Errors.Count == 0
                    || res.JSON.Errors[0].Count == 0
                    || (!res.JSON.Errors[0][0].Equals("NO_INVITE_FOUND")  // This appears to be an API bug, as this is sometimes returned with a success response.  --Kris
                        && !res.JSON.Errors[0][0].Equals("ALREADY_MODERATOR")))
                {
                    throw ex;
                }
            }

            try
            {
                reddit2.Models.Moderation.LeaveModerator("t2_" + patsy.Id, testData["Subreddit"]);
            }
            catch (RedditForbiddenException) { }
        }

        [TestMethod]
        public void Distinguish()
        {
            PostResultShortContainer postResult = TestPost();

            Validate(postResult);

            CommentResultContainer commentResult = TestComment(postResult.JSON.Data.Name);

            Validate(commentResult);

            PostResultContainer post = reddit.Models.Moderation.DistinguishPost("yes", postResult.JSON.Data.Name);
            CommentResultContainer comment = reddit.Models.Moderation.DistinguishComment("yes", commentResult.JSON.Data.Things[0].Data.Name, true);

            Validate(post);
            Assert.AreEqual(postResult.JSON.Data.Name, post.JSON.Data.Things[0].Data.Name);
            Assert.AreEqual("moderator", post.JSON.Data.Things[0].Data.Distinguished);

            Validate(comment);
            Assert.AreEqual(commentResult.JSON.Data.Things[0].Data.Name, comment.JSON.Data.Things[0].Data.Name);
            Assert.AreEqual("moderator", comment.JSON.Data.Things[0].Data.Distinguished);
            Assert.IsTrue(comment.JSON.Data.Things[0].Data.Stickied);
        }

        [TestMethod]
        public void IgnoreReports()
        {
            PostResultShortContainer postResult = TestPost();

            Validate(postResult);

            CommentResultContainer commentResult = TestComment(postResult.JSON.Data.Name);

            Validate(commentResult);

            reddit.Models.Moderation.IgnoreReports(postResult.JSON.Data.Name);
            reddit.Models.Moderation.IgnoreReports(commentResult.JSON.Data.Things[0].Data.Name);

            reddit.Models.Moderation.UnignoreReports(postResult.JSON.Data.Name);
            reddit.Models.Moderation.UnignoreReports(commentResult.JSON.Data.Things[0].Data.Name);
        }

        [TestMethod]
        public void Contributor()
        {
            User patsy = GetTargetUserModel();

            Validate(patsy);

            GenericContainer res = reddit.Models.Users.Friend(new UsersFriendInput(patsy.Name, "contributor", permissions: ""), testData["Subreddit"]);

            Validate(res);

            reddit2.Models.Moderation.LeaveContributor(reddit2.Models.Subreddits.About(testData["Subreddit"]).Data.Name);
        }

        [TestMethod]
        public void Remove()
        {
            PostResultShortContainer postResult = TestPost();

            Validate(postResult);

            CommentResultContainer commentResult = TestComment(postResult.JSON.Data.Name);

            Validate(commentResult);

            reddit.Models.Moderation.Remove(new ModerationRemoveInput(postResult.JSON.Data.Name));
            reddit.Models.Moderation.Remove(new ModerationRemoveInput(commentResult.JSON.Data.Things[0].Data.Name));
        }
    }
}
