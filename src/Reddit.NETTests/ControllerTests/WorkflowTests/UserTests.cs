using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RedditTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class UserTests : BaseTests
    {
        private Dictionary<string, Post> NewPosts;
        private Dictionary<string, Comment> NewComments;

        private Subreddit Subreddit
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

        public UserTests() : base()
        {
            NewPosts = new Dictionary<string, Post>();
            NewComments = new Dictionary<string, Comment>();
        }

        private Subreddit GetSubreddit()
        {
            Subreddit = reddit.Subreddit(testData["Subreddit"]).About();
            return Subreddit;
        }

        [TestMethod]
        public void FriendAndUnfriend()
        {
            User patsy = reddit.User(GetTargetUserModel());

            patsy.AddRelationship("", "", "", "", 999, "+mail", "moderator_invite", testData["Subreddit"]);
            patsy.RemoveRelationship("moderator_invite", "", testData["Subreddit"]);
        }

        [TestMethod]
        public void MonitorPostAndCommentHistory()
        {
            // Monitor new posts for 2 minutes.  --Kris
            reddit.Account.Me.GetPostHistory(sort: "newForced");
            reddit.Account.Me.MonitorPostHistory(monitoringExpiration: DateTime.Now.AddMinutes(2));
            reddit.Account.Me.PostHistoryUpdated += C_NewPostsUpdated;

            // Monitor new comments for 2 minutes.  --Kris
            reddit.Account.Me.GetCommentHistory();
            reddit.Account.Me.MonitorCommentHistory(monitoringExpiration: DateTime.Now.AddMinutes(2));
            reddit.Account.Me.CommentHistoryUpdated += C_NewCommentsUpdated;

            LinkPost post = Subreddit.LinkPost("Test Link Post", "https://www.nuget.org/packages/Reddit").Submit(resubmit: true);

            Validate(post);

            Comment comment = post.Reply("Test comment.");

            Validate(comment);

            DateTime start = DateTime.Now;
            while (NewPosts.Count == 0
                && NewComments.Count == 0
                && start.AddMinutes(2) > DateTime.Now)
            {
                Thread.Sleep(1000);
            }
        }

        private void C_NewPostsUpdated(object sender, PostsUpdateEventArgs e)
        {
            foreach (Post post in e.Added)
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
