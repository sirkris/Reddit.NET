using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests
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
