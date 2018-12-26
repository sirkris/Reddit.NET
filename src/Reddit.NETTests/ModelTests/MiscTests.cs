using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Things;
using System.Collections.Generic;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class MiscTests : BaseTests
    {
        public MiscTests() : base() { }

        [TestMethod]
        public void Scopes()
        {
            Dictionary<string, Scope> scopes = reddit.Models.Misc.Scopes();

            Assert.IsNotNull(scopes);
            Assert.IsTrue(scopes.Count > 0);
        }

        [TestMethod]
        public void SavedMediaText()
        {
            Dictionary<string, string> res = reddit.Models.Misc.SavedMediaText("https://e.thumbs.redditmedia.com/bOToSJt13ylszjE4.png", "pics");

            Assert.IsNotNull(res);
        }
    }
}
