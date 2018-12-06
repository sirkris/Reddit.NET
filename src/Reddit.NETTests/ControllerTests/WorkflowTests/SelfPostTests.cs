using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Controllers;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ControllerTests.WorkflowTests
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

        public SelfPost TestSelfPost()
        {
            return reddit.Subreddit(testData["Subreddit"]).SelfPost("Test Self Post", "It is now: " + DateTime.Now.ToString("F")).Submit();
        }

        [TestMethod]
        public void Submit()
        {
            Validate(Post);
        }
    }
}
