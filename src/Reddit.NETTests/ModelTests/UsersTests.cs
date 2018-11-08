using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class UsersTests : BaseTests
    {
        [TestMethod]
        public void UserDataByAccountIds()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

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
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            bool? available = reddit.Models.Users.UsernameAvailable("KrisCraig");

            Assert.IsTrue(available.HasValue);
            Assert.IsFalse(available.Value);
        }

        [TestMethod]
        public void AvailableUsernameAvailable()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            bool? available = reddit.Models.Users.UsernameAvailable(DateTime.Now.ToString("ddd-dd-MMM-yyyy-ffff"));

            Assert.IsTrue(available.HasValue);
            Assert.IsTrue(available.Value);
        }

        [TestMethod]
        public void InvalidUsernameAvailable()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            bool? available = reddit.Models.Users.UsernameAvailable("ThisUserDoesNotExistAndEvenIfHeDoesThenHeProbablyFucksChickensOrSomething");

            Assert.IsNull(available);
        }
        
        [TestMethod]
        public void Trophies()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            TrophyList trophies = reddit.Models.Users.Trophies("KrisCraig");

            Assert.IsNotNull(trophies);
            Assert.IsTrue(trophies.Data.Trophies.Count > 0);
        }

        [TestMethod]
        public void About()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            UserChild user = reddit.Models.Users.About("KrisCraig");

            Assert.IsNotNull(user);
            Assert.IsTrue(user.Data.Name.Equals("KrisCraig"));
        }

        // Requires existing test subreddit with mod access.  --Kris
        [TestMethod]
        public void FriendInviteMod()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            GenericContainer res = reddit.Models.Users.Friend(null, null, null, null, 999, "RedditDotNetBot", "+mail", "moderator_invite", testData["Subreddit"]);

            // While we're at it, let's use this opportunity to test the SetPermissions endpoint.  --Kris
            GenericContainer res2 = reddit.Models.Users.SetPermissions("RedditDotNetBot", "+wiki", "moderator_invite", testData["Subreddit"]);

            Validate(res);
            Validate(res2);
        }

        [TestMethod]
        public void PostHistoryOverview()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "overview", 10, "given", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void PostHistorySubmitted()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "submitted", 10, "given", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void PostHistoryUpvoted()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "upvoted", 10, "given", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void PostHistoryDownvoted()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "downvoted", 10, "given", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void PostHistoryHidden()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "hidden", 10, "given", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void PostHistorySaved()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "saved", 10, "given", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void PostHistoryGilded()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer history = reddit.Models.Users.PostHistory("KrisCraig", "gilded", 10, "given", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void CommentHistoryComments()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            CommentContainer history = reddit.Models.Users.CommentHistory("KrisCraig", "comments", 10, "given", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void CommentHistorySaved()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            CommentContainer history = reddit.Models.Users.CommentHistory("KrisCraig", "saved", 10, "given", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void CommentHistoryGilded()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            CommentContainer history = reddit.Models.Users.CommentHistory("KrisCraig", "gilded", 10, "given", "top", "all", null, null, false);

            Validate(history);
        }

        [TestMethod]
        public void BlockAndUnblock()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            // Block user.
            UserActionResult res = reddit.Models.Users.BlockUser("", "RedditDotNetBot");

            // Unblock user (returns empty JSON).
            reddit.Models.Users.Unfriend("t2_6vsit", "", "RedditDotNetBot", "enemy");

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void Friendship()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            // Add a friend.
            UserActionResult updateRes = reddit.Models.Users.UpdateFriend("RedditDotNetBot", "{}");

            // Get data on an existing friend.
            UserActionResult getRes = reddit.Models.Users.GetFriend("RedditDotNetBot");

            // It's just not working out.  Delete the friend and burn all their stuff.
            reddit.Models.Users.DeleteFriend("RedditDotNetBot");

            Assert.IsNotNull(updateRes);
            Assert.IsNotNull(updateRes.Name);
            Assert.IsTrue(updateRes.Name.Equals("RedditDotNetBot"));

            Assert.IsNotNull(getRes);
            Assert.IsNotNull(getRes.Name);
            Assert.IsTrue(getRes.Name.Equals("RedditDotNetBot"));
        }
    }
}
