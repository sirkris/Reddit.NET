using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            GenericContainer res = reddit.Models.Users.Friend(null, null, null, null, 999, "RedditDotNetBot", "+mail", "moderator_invite", testData["Subreddit"]);

            // While we're at it, let's use this opportunity to test the SetPermissions endpoint.  --Kris
            GenericContainer res2 = reddit.Models.Users.SetPermissions("RedditDotNetBot", "+wiki", "moderator_invite", testData["Subreddit"]);

            Validate(res);
            Validate(res2);
        }

        [TestMethod]
        public void PostHistoryOverview()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "overview", 10, "all", "", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void PostHistorySubmitted()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "submitted", 10, "all", null, "all", "", "", false);

            Validate(history);
        }

        [TestMethod]
        public void PostHistoryUpvoted()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "upvoted", 10, "all", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void PostHistoryDownvoted()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "downvoted", 10, "all", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void PostHistoryHidden()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "hidden", 10, "all", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void PostHistorySaved()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "saved", 10, "all", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void PostHistoryGilded()
        {
            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "gilded", 10, "all", "top", "all", null, null, false);

            Validate(history, true);
        }

        [TestMethod]
        public void CommentHistoryComments()
        {
            CommentContainer history = reddit.Models.Users.CommentHistory("KrisCraig", "comments", 10, "all", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void CommentHistorySaved()
        {
            CommentContainer history = reddit.Models.Users.CommentHistory("KrisCraig", "saved", 10, "all", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void CommentHistoryGilded()
        {
            CommentContainer history = reddit.Models.Users.CommentHistory("KrisCraig", "gilded", 10, "all", "top", "all", null, null, false);

            Validate(history, true);
        }

        [TestMethod]
        public void BlockAndUnblock()
        {
            // Block user.
            UserActionResult res = reddit.Models.Users.BlockUser("", "RedditDotNetBot");

            // Unblock user (returns empty JSON).
            reddit.Models.Users.Unfriend("t2_6vsit", "", "RedditDotNetBot", "enemy");

            Assert.IsNotNull(res);
        }
    }
}
