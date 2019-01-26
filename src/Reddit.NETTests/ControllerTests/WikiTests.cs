using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;

namespace RedditTests.ControllerTests
{
    [TestClass]
    public class WikiTests : BaseTests
    {
        private Subreddit Subreddit
        {
            get
            {
                return subreddit ?? GetSubreddit(ref subreddit);
            }
            set
            {
                subreddit = value;
            }
        }
        private Subreddit subreddit;

        private WikiPage Index
        {
            get
            {
                return index ?? GetIndex();
            }
            set
            {
                index = value;
            }
        }
        private WikiPage index;

        public WikiTests() : base() { }

        private WikiPage GetIndex()
        {
            Index = Subreddit.Wiki.GetPage("index");
            return Index;
        }

        [TestMethod]
        public void About()
        {
            Validate(Index);
            Assert.AreEqual("index", Index.Name);
        }

        [TestMethod]
        public void Pages()
        {
            Validate(Subreddit.Wiki.Pages);
        }

        [TestMethod]
        public void GetPage()
        {
            Validate(Subreddit.Wiki.GetPage("index"));
        }

        [TestMethod]
        public void GetContributors()
        {
            Validate(Subreddit.Wiki.GetContributors());
        }

        [TestMethod]
        public void GetBannedUsers()
        {
            Validate(Subreddit.Wiki.GetBannedUsers());
        }

        [TestMethod]
        public void GetRecentPageRevisions()
        {
            Validate(Subreddit.Wiki.GetRecentPageRevisions());
        }

        [TestMethod]
        public void Revisions()
        {
            Validate(Index.Revisions());
        }

        [TestMethod]
        public void GetPermissions()
        {
            Validate(Index.GetPermissions());
        }

        [TestMethod]
        public void UpdatePermissions()
        {
            Validate(Index.UpdatePermissions(true, 0));
        }
    }
}
