using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Controllers;
using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Reddit.NETTests.ControllerTests.WorkflowTests.StressTests
{
    [TestClass]
    public class AsyncTests : BaseStressTests
    {
        public AsyncTests() : base()
        {
            NewPosts = new Dictionary<string, LinkPost>();
            NewComments = new Dictionary<string, Comment>();
            NewCommentsByThread = new Dictionary<string, List<Comment>>();
        }

        private Dictionary<string, LinkPost> NewPosts;
        private Dictionary<string, Comment> NewComments;
        private Dictionary<string, List<Comment>> NewCommentsByThread;

        [TestMethod]
        public void Timing()
        {
            DateTime start = DateTime.Now;
            for (int i = 1; i <= 60; i++)
            {
                // Despite what VS says, we don't want to use await here.  --Kris
                SelfPost.Comment("Stress test comment #" + i.ToString()).SubmitAsync();
            }
            DateTime end = DateTime.Now;

            // In my tests, it took roughly 3 seconds to loop through all those async calls, so 10 should be more than enough.  --Kris
            Assert.IsTrue(start.AddSeconds(10) > end);

            // Wait a bit, then see if all the comments were added.  We won't rely on monitoring for this one.  --Kris
            start = DateTime.Now;
            while (start.AddMinutes(5) > DateTime.Now) { }

            Assert.AreEqual(60, SelfPost.Comments.New.Count);
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

            // Create 50 posts, each with 10 comments.  --Kris
            List<LinkPost> posts = new List<LinkPost>();
            for (int i = 1; i <= 50; i++)
            {
                posts.Add(LinkPost.Submit(resubmit: true));  // Add .About() after the Submit call if you want more than just the fullname/id of the new post.  --Kris

                posts[i - 1].Comments.MonitorNew();
                posts[i - 1].Comments.NewUpdated += C_NewCommentsUpdated;

                for (int ii = 1; ii <= 10; ii++)
                {
                    // Despite what VS says, we don't want to use await here.  --Kris
                    posts[i - 1].Comment("Stress test comment #" + i.ToString() + "-" + ii.ToString()).SubmitAsync();
                }
            }

            // We're deliberately flooding it with requests here (550 total, plus monitoring), so it may take awhile for the test to complete.  --Kris
            DateTime start = DateTime.Now;
            while ((NewPosts.Count < 50
                || NewComments.Count < 500)
                && start.AddHours(1) > DateTime.Now) { }

            Assert.IsTrue(NewPosts.Count >= 50);
            Assert.IsTrue(NewComments.Count >= 500);
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

                    /*if (!NewCommentsByThread.ContainsKey(comment.Root.Fullname))
                    {
                        NewCommentsByThread.Add(comment.Root.Fullname, new List<Comment>());
                    }
                    NewCommentsByThread[comment.Root.Fullname].Add(comment);*/
                }
            }
        }
    }
}
