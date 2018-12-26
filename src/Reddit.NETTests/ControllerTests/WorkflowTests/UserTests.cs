using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;

namespace RedditTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class UserTests : BaseTests
    {
        public UserTests() : base() { }

        [TestMethod]
        public void FriendAndUnfriend()
        {
            User patsy = reddit.User(GetTargetUserModel());

            patsy.AddRelationship("", "", "", "", 999, "+mail", "moderator_invite", testData["Subreddit"]);
            patsy.RemoveRelationship("", "moderator_invite", testData["Subreddit"]);
        }
    }
}
