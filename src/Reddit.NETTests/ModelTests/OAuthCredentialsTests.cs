using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class OAuthCredentialsTests : BaseTests
    {
        public OAuthCredentialsTests() : base() { }

        [TestMethod]
        public void AccessToken()
        {
            reddit.Models.Account.Me();

            Assert.IsNotNull(reddit.Models.OAuthCredentials.AccessToken);
            Assert.AreNotEqual(reddit.Models.OAuthCredentials.AccessToken, "null");
        }
    }
}
