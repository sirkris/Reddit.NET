using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Exceptions;
using Reddit.NET.Models.Structures;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class AppOnlyTests : BaseTests
    {
        [TestMethod]
        public void Emoji()
        {
            Validate(reddit3.Models.Emoji.All("WayOfTheBern"));
        }

        [TestMethod]
        public void Flair()
        {
            bool caught = false;
            try
            {
                // This will fail because the UserFlair endpoint requires an authenticated user.  --Kris
                Validate(reddit3.Models.Flair.UserFlair(testData["Subreddit"]));
            }
            catch (RedditUserRequiredException)
            {
                caught = true;
            }
            Assert.IsTrue(caught);
        }

        [TestMethod]
        public void LinksAndComments()
        {
            Validate(reddit3.Models.LinksAndComments.MoreChildren("dlpnw9j", false, "t3_6tyfna", "new"));
        }

        [TestMethod]
        public void Misc()
        {
            Validate(reddit3.Models.Misc.Scopes());
            Validate(reddit3.Models.Misc.SavedMediaText("https://e.thumbs.redditmedia.com/bOToSJt13ylszjE4.png", "pics"));
        }

        [TestMethod]
        public void Multis()
        {
            Validate(reddit3.Models.Multis.User("KrisCraig", false));
            Validate(reddit3.Models.Multis.Get("user/KrisCraig/m/unitedprogressives", false));
        }
    }
}
