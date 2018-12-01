using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests.WorkflowTests
{
    [TestClass]
    public class UsersTests : BaseTests
    {
        public UsersTests() : base() { }

        [TestMethod]
        public void Friendship()
        {
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
