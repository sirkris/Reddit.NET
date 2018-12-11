using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Controllers;
using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class SubredditTests : BaseTests
    {
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

        private Dictionary<string, Post> NewPosts;

        public SubredditTests() : base()
        {
            NewPosts = new Dictionary<string, Post>();
        }

        private Subreddit GetSubreddit()
        {
            Subreddit = reddit.Subreddit(testData["Subreddit"]).About();
            return Subreddit;
        }

        [TestMethod]
        public void SubredditImages()
        {
            // Get the embedded test images.  --Kris
            byte[] imageData = GetResourceFile("birdie.png");
            byte[] imageBannerData = GetResourceFile("banner.jpg");
            byte[] imageIconData = GetResourceFile("birdie256.jpg");

            // Add the images.  --Kris
            Validate(Subreddit.UploadHeader(imageData));
            Validate(Subreddit.UploadImg(imageData, "birdie"));
            Validate(Subreddit.UploadIcon(imageIconData, "jpg"));
            Validate(Subreddit.UploadBanner(imageBannerData, "jpg"));

            // Delete the images.  --Kris
            Subreddit.DeleteHeader();
            Subreddit.DeleteImg("birdie");
            Subreddit.DeleteIcon();
            Subreddit.DeleteBanner();
        }

        [TestMethod]
        public void ModeratorInvite()
        {
            User patsy = GetTargetUser();

            reddit.User(patsy).AddRelationship("", "", "", "", 999, "+wiki", "moderator_invite", Subreddit.Name);

            reddit2.Subreddit(Subreddit.Name).AcceptModeratorInvite();
            reddit2.Subreddit(Subreddit.Name, fullname: Subreddit.Fullname).LeaveModerator();
        }

        [TestMethod]
        public void ContributorInvite()
        {
            User patsy = GetTargetUser();

            reddit.User(patsy).AddRelationship("", "", "", "", 999, "+wiki", "contributor", Subreddit.Name);

            reddit2.Subreddit(Subreddit.Name, fullname: Subreddit.Fullname).LeaveContributor();
        }

        [TestMethod]
        public void MonitorNewPosts()
        {
            Subreddit.Posts.GetNew();  // This call prevents any existing "new"-sorted posts from triggering the update event.  --Kris
            Subreddit.Posts.MonitorNew();
            Subreddit.Posts.NewUpdated += C_NewPostsUpdated;

            for (int i = 1; i <= 5; i++)
            {
                // Despite what VS says, we don't want to use await here.  --Kris
                Subreddit.LinkPost("Test Link Post #" + i.ToString(), "https://github.com/sirkris/Reddit.NET").SubmitAsync(true);
            }

            for (int i = 1; i <= 5; i++)
            {
                // Despite what VS says, we don't want to use await here.  --Kris
                Subreddit.SelfPost("Test Self Post #" + i.ToString(), "This is a test post created by [Reddit.NET](https://github.com/sirkris/Reddit.NET).").SubmitAsync();
            }

            DateTime start = DateTime.Now;
            while (NewPosts.Count < 10
                && start.AddMinutes(1) > DateTime.Now) { }

            Subreddit.Posts.NewUpdated -= C_NewPostsUpdated;
            Subreddit.Posts.MonitorNew();

            Assert.AreEqual(10, NewPosts.Count);
        }

        // When a new post is detected in MonitorNewPosts, this method will add it/them to the list.  --Kris
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
    }
}
