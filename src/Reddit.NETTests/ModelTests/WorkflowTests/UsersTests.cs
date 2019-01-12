using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Inputs.Users;
using Reddit.Things;

namespace RedditTests.ModelTests.WorkflowTests
{
    [TestClass]
    public class UsersTests : BaseTests
    {
        public UsersTests() : base() { }

        [TestMethod]
        public void Friendship()
        {
            // Add a friend.
            UserActionResult updateRes = reddit.Models.Users.UpdateFriend("RedditDotNetBot");

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

        [TestMethod]
        public void FriendAndUnfriend()
        {
            User me = reddit.Models.Account.Me();
            User patsy = GetTargetUserModel();

            string myFullname = "t2_" + me.Id;
            string patsyFullname = "t2_" + patsy.Id;

            Validate(reddit.Models.Users.Friend(new UsersFriendInput(patsy.Name, "moderator_invite"), testData["Subreddit"]));
            reddit.Models.Users.Unfriend(new UsersUnfriendInput(patsy.Name, patsyFullname, "moderator_invite"), testData["Subreddit"]);
        }
    }
}
