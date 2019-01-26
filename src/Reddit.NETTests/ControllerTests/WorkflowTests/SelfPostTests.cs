using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;
using System;

namespace RedditTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class SelfPostTests : BaseTests
    {
        private SelfPost Post
        {
            get
            {
                return post ?? TestSelfPost();
            }
            set
            {
                post = value;
            }
        }
        private SelfPost post;

        public SelfPostTests() : base() { }

        private SelfPost TestSelfPost()
        {
            Post = reddit.Subreddit(testData["Subreddit"]).SelfPost("Test Self Post", "It is now: " + DateTime.Now.ToString("F")).Submit();
            return Post;
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
