using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Exceptions;
using Reddit.Inputs.LinksAndComments;
using System;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class AppOnlyTests : BaseTests
    {
        [TestMethod]
        public void Emoji()
        {
            // Sometimes the API lets us hit this unauthenticated, other times it returns an error saying you have to be logged in.  Inconsistency FTW!  --Kris
            try
            {
                Validate(reddit3.Models.Emoji.All("WayOfTheBern"));
            }
            catch (RedditUserRequiredException) { }
            catch (AggregateException ex) when (ex.InnerException is RedditUserRequiredException) { }
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
            catch (AggregateException ex) when (ex.InnerException is RedditUserRequiredException)
            {
                caught = true;
            }
            Assert.IsTrue(caught);
        }

        [TestMethod]
        public void LinksAndComments()
        {
            Validate(reddit3.Models.LinksAndComments.MoreChildren(new LinksAndCommentsMoreChildrenInput("dlpnw9j", false, "t3_6tyfna", "new")));
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
