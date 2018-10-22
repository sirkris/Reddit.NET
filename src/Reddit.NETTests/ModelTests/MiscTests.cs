using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class MiscTests : BaseTests
    {
        [TestMethod]
        public void Scopes()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            Dictionary<string, Scope> scopes = reddit.Models.Misc.Scopes();

            Assert.IsNotNull(scopes);
            Assert.IsTrue(scopes.Count > 0);
        }
    }
}
