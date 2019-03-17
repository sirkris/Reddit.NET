using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Inputs.Search;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class SearchTests : BaseTests
    {
        public SearchTests() : base() { }

        [TestMethod]
        public void SearchSubreddits()
        {
            Validate(reddit.Models.Search.SearchSubreddits(new SearchGetSearchInput("Bernie Sanders"), "WayOfTheBern"), 1);
            Validate(reddit.Models.Search.SearchSubreddits(new SearchGetSearchInput("Bernie Sanders")), 1);
        }

        [TestMethod]
        public void SearchPosts()
        {
            Validate(reddit.Models.Search.SearchPosts(new SearchGetSearchInput("Bernie Sanders"), "WayOfTheBern"), 1);
            Validate(reddit.Models.Search.SearchPosts(new SearchGetSearchInput("Bernie Sanders")), 1);
        }

        [TestMethod]
        public void SearchUsers()
        {
            Validate(reddit.Models.Search.SearchUsers(new SearchGetSearchInput("Trump")));
        }

        [TestMethod]
        public void MultiSearch()
        {
            Validate(reddit.Models.Search.MultiSearch(new SearchGetSearchInput("Trump", type: "link,sr,user")));
        }
    }
}
