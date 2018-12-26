using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;

namespace RedditTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class AccountTests : BaseTests
    {
        public AccountTests() : base() { }

        [TestMethod]
        public void Friendship()
        {
            User patsy = GetTargetUser();

            // Add a friend.
            Validate(reddit.Account.UpdateFriend(patsy.Name));

            // Get data on an existing friend.
            Validate(reddit.Account.GetFriend(patsy.Name));

            // It's just not working out.  Delete the friend and burn all their stuff.
            reddit.Account.DeleteFriend(patsy.Name);
        }
    }
}
