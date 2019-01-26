using Reddit.Controllers;
using System;
using System.Threading;

namespace RedditTests.ControllerTests.WorkflowTests.StressTests
{
    public abstract class BaseStressTests : BaseTests
    {
        protected Subreddit Subreddit
        {
            get
            {
                return subreddit ?? GetSubreddit();
            }
            set
            {
                subreddit = value;
            }
        }
        private Subreddit subreddit;

        protected LinkPost LinkPost
        {
            get
            {
                return linkPost ?? TestLinkPost();
            }
            set
            {
                linkPost = value;
            }
        }
        private LinkPost linkPost;

        protected SelfPost SelfPost
        {
            get
            {
                return selfPost ?? TestSelfPost();
            }
            set
            {
                selfPost = value;
            }
        }
        private SelfPost selfPost;

        public BaseStressTests() : base()
        {
            // Wait until it has been at least a minute since the last request before beginning each stress test.  --Kris
            Thread.Sleep(60000);
        }

        private Subreddit GetSubreddit()
        {
            Subreddit = reddit.Subreddit(testData["Subreddit"]).About();
            return Subreddit;
        }

        private LinkPost TestLinkPost()
        {
            LinkPost = reddit.Subreddit(testData["Subreddit"]).LinkPost("Stress Test Link Post", "http://www.go-fuck-yourself.com").Submit(resubmit: true);
            return LinkPost;
        }

        private SelfPost TestSelfPost()
        {
            SelfPost = reddit.Subreddit(testData["Subreddit"]).SelfPost("Stress Test Self Post", "It is now: " + DateTime.Now.ToString("F")).Submit();
            return SelfPost;
        }
    }
}
