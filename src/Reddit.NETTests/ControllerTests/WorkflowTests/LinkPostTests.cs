using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;

namespace RedditTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class LinkPostTests : BaseTests
    {
        private LinkPost Post
        {
            get
            {
                return post ?? TestLinkPost();
            }
            set
            {
                post = value;
            }
        }
        private LinkPost post;

        public LinkPostTests() : base() { }

        private LinkPost TestLinkPost()
        {
            Post = reddit.Subreddit(testData["Subreddit"]).LinkPost("Test Link Post", "http://www.go-fuck-yourself.com").Submit(resubmit: true);
            return post;
        }

        [TestMethod]
        public void Submit()
        {
            Validate(Post);
        }

        [TestMethod]
        public void Distinguish()
        {
            Post.Distinguish("yes");
        }

        [TestMethod]
        public void Remove()
        {
            Post.Remove();
        }
    }
}
