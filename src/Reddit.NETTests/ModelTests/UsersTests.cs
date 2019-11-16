using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Inputs.Users;
using Reddit.Things;
using System;
using System.Collections.Generic;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class UsersTests : BaseTests
    {
        public UsersTests() : base() { }

        [TestMethod]
        public void UserDataByAccountIds()
        {
            Dictionary<string, UserSummary> res = reddit.Models.Users.UserDataByAccountIds("t2_6vsit,t2_2cclzaxt");

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count == 2);
            Assert.IsTrue(res.ContainsKey("t2_6vsit"));
            Assert.IsTrue(res.ContainsKey("t2_2cclzaxt"));
            Assert.IsTrue(res["t2_6vsit"].Name.Equals("KrisCraig"));
            Assert.IsTrue(res["t2_2cclzaxt"].Name.Equals("RedditDotNetBot"));
        }

        [TestMethod]
        public void ExistingUsernameAvailable()
        {
            bool? available = reddit.Models.Users.UsernameAvailable("KrisCraig");

            Assert.IsTrue(available.HasValue);
            Assert.IsFalse(available.Value);
        }

        [TestMethod]
        public void AvailableUsernameAvailable()
        {
            bool? available = reddit.Models.Users.UsernameAvailable(DateTime.Now.ToString("ddd-dd-MMM-yyyy-ffff"));

            Assert.IsTrue(available.HasValue);
            Assert.IsTrue(available.Value);
        }

        [TestMethod]
        public void InvalidUsernameAvailable()
        {
            bool? available = reddit.Models.Users.UsernameAvailable("ThisUserDoesNotExistAndEvenIfHeDoesThenHeProbablyFucksChickensOrSomething");

            Assert.IsNull(available);
        }
        
        [TestMethod]
        public void Trophies()
        {
            TrophyList trophies = reddit.Models.Users.Trophies("KrisCraig");

            Assert.IsNotNull(trophies);
            Assert.IsTrue(trophies.Data.Trophies.Count > 0);
        }

        [TestMethod]
        public void About()
        {
            UserChild user = reddit.Models.Users.About("KrisCraig");

            Assert.IsNotNull(user);
            Assert.IsTrue(user.Data.Name.Equals("KrisCraig"));
        }

        // Requires existing test subreddit with mod access.  --Kris
        [TestMethod]
        public void FriendInviteMod()
        {
            GenericContainer res = reddit.Models.Users.Friend(new UsersFriendInput("RedditDotNetBot", "moderator_invite"), testData["Subreddit"]);

            // While we're at it, let's use this opportunity to test the SetPermissions endpoint.  --Kris
            GenericContainer res2 = reddit.Models.Users.SetPermissions(new UsersSetPermissionsInput("RedditDotNetBot", "+wiki", "moderator_invite"), testData["Subreddit"]);

            Validate(res);
            Validate(res2);
        }

        [TestMethod]
        public void ModeratedSubreddits()
        {
            Validate(reddit.Models.Users.ModeratedSubreddits("KrisCraig", new UsersHistoryInput()));
        }

        [TestMethod]
        public void Overview()
        {
            Validate(reddit.Models.Users.Overview("KrisCraig", new UsersHistoryInput()));
        }

        [TestMethod]
        [Obsolete("Obtaining overview data using this method is no longer recommended.  Please use " + nameof(Reddit.Models.Users.Overview) + " instead.")]
        public void PostHistoryOverview()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "overview", new UsersHistoryInput(context: 10));

            Validate(history);
        }

        [TestMethod]
        public void PostHistorySubmitted()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "submitted", new UsersHistoryInput(context: 10));

            Validate(history);
        }

        [TestMethod]
        public void PostHistoryUpvoted()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "upvoted", new UsersHistoryInput(sort: "top", context: 10));

            Validate(history);
        }

        [TestMethod]
        public void PostHistoryDownvoted()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "downvoted", new UsersHistoryInput(sort: "top", context: 10));

            Validate(history);
        }

        [TestMethod]
        public void PostHistoryHidden()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "hidden", new UsersHistoryInput(sort: "top", context: 10));

            Validate(history);
        }

        [TestMethod]
        public void PostHistorySaved()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "saved", new UsersHistoryInput(sort: "top", context: 10));

            Validate(history);
        }

        [TestMethod]
        public void PostHistoryGilded()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "gilded", new UsersHistoryInput(sort: "top", context: 10));

            Validate(history, true);
        }

        [TestMethod]
        public void CommentHistoryComments()
        {
            CommentContainer history = reddit.Models.Users.CommentHistory("KrisCraig", "comments", new UsersHistoryInput(type: "comments", sort: "top", context: 10));

            Validate(history);
        }

        [TestMethod]
        public void CommentHistorySaved()
        {
            CommentContainer history = reddit.Models.Users.CommentHistory("KrisCraig", "saved", new UsersHistoryInput(type: "comments", sort: "top", context: 10));

            Validate(history);
        }

        [TestMethod]
        public void CommentHistoryGilded()
        {
            CommentContainer history = reddit.Models.Users.CommentHistory("KrisCraig", "gilded", new UsersHistoryInput(type: "comments", sort: "top", context: 10));

            Validate(history, true);
        }

        [TestMethod]
        public void BlockAndUnblock()
        {
            // TODO - Unblock doesn't seem to actually do anything.  Skip this test for now so as not to block the workflow tests.  --Kris
            return;
            // Block user.
            UserActionResult res = reddit.Models.Users.BlockUser(new UsersBlockUserInput(name: "RedditDotNetBot"));

            // Unblock user (returns empty JSON).
            reddit.Models.Users.Unfriend(new UsersUnfriendInput("RedditDotNetBot", "t2_6vsit", "enemy"));

            Assert.IsNotNull(res);
        }
    }
}
