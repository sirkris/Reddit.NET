using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using System;
using System.Collections.Generic;

namespace RedditTests.ControllerTests.WorkflowTests.StressTests
{
    [TestClass]
    public class AsyncTests : BaseStressTests
    {
        public AsyncTests() : base()
        {
            NewPosts = new Dictionary<string, LinkPost>();
            NewComments = new Dictionary<string, Comment>();
        }

        private Dictionary<string, LinkPost> NewPosts;
        private Dictionary<string, Comment> NewComments;

        [TestMethod]
        public void Timing()
        {
            DateTime start = DateTime.Now;
            for (int i = 1; i <= 60; i++)
            {
                // Despite what VS says, we don't want to use await here.  --Kris
                SelfPost.ReplyAsync("Stress test comment #" + i.ToString());
            }
            DateTime end = DateTime.Now;

            // Wait a bit, then see if all the comments were added.  We won't rely on monitoring for this one.  --Kris
            start = DateTime.Now;
            while (start.AddMinutes(5) > DateTime.Now) { }

            Assert.IsTrue(SelfPost.Comments.New.Count >= 60);
        }

        [TestMethod]
        public void SpeedLimit()
        {
            Subreddit.Posts.GetNew();
            Subreddit.Posts.MonitorNew();
            Subreddit.Posts.NewUpdated += C_NewPostsUpdated;

            // 60 requests are allowed per minute.  --Kris
            for (int i = 1; i <= 100; i++)
            {
                // Despite what VS says, we don't want to use await here.  --Kris
                LinkPost.SubmitAsync(resubmit: true);
            }

            // Note - The best way to verify the speed limit is working is to debug this test and observe the events relative to the timing.  --Kris

            // Make sure the remaining posts eventually trickle in.  --Kris
            DateTime start = DateTime.Now;
            while (NewPosts.Count < 100
                && start.AddMinutes(5) > DateTime.Now) { }

            Subreddit.Posts.NewUpdated -= C_NewPostsUpdated;
            Subreddit.Posts.MonitorNew();

            Assert.IsTrue(NewPosts.Count >= 100);
        }

        [TestMethod]
        public void PoliceState()
        {
            // Monitor all kinds of shit.  --Kris
            Subreddit.Posts.GetNew();
            Subreddit.Posts.MonitorNew();
            Subreddit.Posts.NewUpdated += C_NewPostsUpdated;

            // Create 60 posts, each with 10 comments.  --Kris
            List<LinkPost> posts = new List<LinkPost>();
            for (int i = 1; i <= 60; i++)
            {
                posts.Add(LinkPost.Submit(resubmit: true));  // Add .About() after the Submit call if you want more than just the fullname/id of the new post.  --Kris

                posts[i - 1].Comments.GetNew();
                posts[i - 1].Comments.MonitorNew();
                posts[i - 1].Comments.NewUpdated += C_NewCommentsUpdated;

                for (int ii = 1; ii <= 10; ii++)
                {
                    // Despite what VS says, we don't want to use await here.  --Kris
                    posts[i - 1].ReplyAsync("Stress test comment #" + i.ToString() + "-" + ii.ToString());
                }
            }

            // We're deliberately flooding it with requests here (660 total, plus monitoring), so it may take awhile for the test to complete.  --Kris
            DateTime start = DateTime.Now;
            while ((NewPosts.Count < 60
                || NewComments.Count < 600)
                && start.AddHours(1) > DateTime.Now) { }

            Assert.IsTrue(NewPosts.Count >= 60);

            /*
             * Occasionally, the Reddit API will correctly report the number of comments in a thread but omit one or more of those comments in the actual results, 
             * even though those comments have not been deleted/etc.  This phenomenon also seems to occur in the web UI (confirmed in both old and redesign), which 
             * means the problem must be on Reddit's end.  My guess is it's related to the heavy query load that this particular stress test generates.
             * 
             * On one such thread (https://www.reddit.com/r/RedditDotNETBot/comments/a7wlfp/stress_test_link_post), only 1 comment was missing, even though 
             * Reddit reported 10 comments (as you can see, there are only 9 comments showing; the first one is missing).  All 9 of those comments appeared in the 
             * thread within a couple minutes of one another.  That was about an hour ago and it's still only showing 9 of the 10 comments.  Same result on other sorts.  
             * Waiting/refreshing the results had no effect.
             * 
             * If I'm right, there's nothing we can do on this end other than adjust the assertion to allow for a certain number of lost comments.  In every test I ran, 
             * over 95% of the comments were able to be retrieved, so we'll allow for up to 10% loss.
             * 
             * --Kris
             */
            Assert.IsTrue(NewComments.Count >= 540);
        }

        private void C_NewPostsUpdated(object sender, PostsUpdateEventArgs e)
        {
            foreach (LinkPost post in e.Added)
            {
                if (!NewPosts.ContainsKey(post.Fullname))
                {
                    NewPosts.Add(post.Fullname, post);
                }
            }
        }

        private void C_NewCommentsUpdated(object sender, CommentsUpdateEventArgs e)
        {
            foreach (Comment comment in e.Added)
            {
                if (!NewComments.ContainsKey(comment.Fullname))
                {
                    NewComments.Add(comment.Fullname, comment);
                }
            }
        }
    }
}
