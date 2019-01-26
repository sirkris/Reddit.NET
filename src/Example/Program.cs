using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using System;
using System.Collections.Generic;

/// <summary>
/// A simple example application that demonstrates some of Reddit.NET's basic features.
/// Only passive operations are performed here.  No posts/etc are created or modified.
/// See the Reddit.NETTests project in this solution for more detailed examples of these 
/// and other endpoints/features.
/// </summary>
namespace Example
{
    /// <summary>
    /// The main class for the example application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main method for the example application.
        /// </summary>
        /// <param name="args">If you're using the VS debugger, go to the Example project properties and
        /// enter your args in the "Application Arguments" field under "Debug" as you would in the CLI</param>
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: Example <Reddit App ID> <Reddit Refresh Token> [Reddit Access Token]");
            }
            else
            {
                string appId = args[0];
                string refreshToken = args[1];
                string accessToken = (args.Length > 2 ? args[2] : null);

                // Initialize the API library instance.  --Kris
                RedditAPI reddit = new RedditAPI(appId: appId, refreshToken: refreshToken, accessToken: accessToken);

                // Get info on the Reddit user authenticated by the OAuth credentials.  --Kris
                User me = reddit.Account.Me;

                Console.WriteLine("Username: " + me.Name);
                Console.WriteLine("Cake Day: " + me.Created.ToString("D"));

                // Get post and comment histories (note that pinned profile posts appear at the top even on new sort; use "newForced" sort as a workaround).  --Kris
                List<Post> postHistory = me.PostHistory(sort: "newForced");
                List<Comment> commentHistory = me.CommentHistory(sort: "new");

                if (postHistory.Count > 0)
                {
                    Console.WriteLine("Most recent post: " + postHistory[0].Title);
                }
                if (commentHistory.Count > 0)
                {
                    Console.WriteLine("Most recent comment: " + commentHistory[0].Body);
                }

                // Create a new subreddit.  --Kris
                //Subreddit newSub = reddit.Subreddit("RDNBotSub", "Test Subreddit", "Test sub created by Reddit.NET", "My sidebar.").Create();

                // Get best posts.  Note that "Best" listings are subreddit-agnostic.  --Kris
                List<Post> bestPosts = reddit.Subreddit().Posts.Best;

                if (bestPosts.Count > 0)
                {
                    Console.WriteLine("Current best post (by " + bestPosts[0].Author + "): [" + bestPosts[0].Subreddit + "] " + bestPosts[0].Title);
                }

                // Get info about a subreddit.  --Kris
                Subreddit sub = reddit.Subreddit("AskReddit").About();

                Console.WriteLine("Subreddit Name: " + sub.Name);
                Console.WriteLine("Subreddit Fullname: " + sub.Fullname);
                Console.WriteLine("Subreddit Title: " + sub.Title);
                Console.WriteLine("Subreddit Description: " + sub.Description);

                // Get new posts from this subreddit.  --Kris
                List<Post> newPosts = sub.Posts.New;

                Console.WriteLine("Retrieved " + newPosts.Count.ToString() + " new posts.");

                // Monitor new posts on this subreddit for a minute.  --Kris
                Console.WriteLine("Monitoring " + sub.Name + " for new posts....");

                sub.Posts.NewUpdated += C_NewPostsUpdated;
                sub.Posts.MonitorNew();  // Toggle on.

                /*
                 * But wait, there's more!  We can monitor posts on multiple subreddits at once (delay is automatically multiplied to keep us under speed the limit).
                 * If you want to see something really crazy, check out Reddit.NETTests.ControllerTests.WorkflowTests.StressTests.AsyncTests.PoliceState().  That 
                 * test creates 60 new posts on the test subreddit (and 10 comments for each post), while simultaneously monitoring each of these 60 posts for new 
                 * comments, all while monitoring the test subreddit for new posts.  Of course, the delay between each monitoring thread's request would be roughly 
                 * 90 seconds in this case (the delay is the number of things being monitored times 1.5 seconds), but the library handles all that for you automatically.
                 * 
                 * I strongly recommend you closely examine the Reddit.NETTests project if you wish to become well-versed in developing using this library.
                 * 
                 * --Kris
                 */
                Subreddit funny = reddit.Subreddit("funny");
                Subreddit worldnews = reddit.Subreddit("worldnews");

                // Before monitoring, let's grab the posts once so we have a point of comparison when identifying new posts that come in.  --Kris
                funny.Posts.GetNew();
                worldnews.Posts.GetNew();

                Console.WriteLine("Monitoring funny for new posts....");

                funny.Posts.NewUpdated += C_NewPostsUpdated;
                funny.Posts.MonitorNew();  // Toggle on.

                Console.WriteLine("Monitoring worldnews for new posts....");

                worldnews.Posts.NewUpdated += C_NewPostsUpdated;
                worldnews.Posts.MonitorNew();  // Toggle on.

                DateTime start = DateTime.Now;
                while (start.AddMinutes(1) > DateTime.Now) { }

                // Stop monitoring new posts.  --Kris
                sub.Posts.MonitorNew();  // Toggle off.
                sub.Posts.NewUpdated -= C_NewPostsUpdated;

                funny.Posts.MonitorNew();  // Toggle off.
                funny.Posts.NewUpdated -= C_NewPostsUpdated;

                worldnews.Posts.MonitorNew();  // Toggle off.
                worldnews.Posts.NewUpdated -= C_NewPostsUpdated;

                Console.WriteLine("Done monitoring!");

                // Grab today's top post in AskReddit and monitor its new comments.  --Kris
                Post post = sub.Posts.GetTop("day")[0];
                post.Comments.GetNew();

                Console.WriteLine("Monitoring today's top post on AskReddit....");

                post.Comments.MonitorNew();  // Toggle on.
                post.Comments.NewUpdated += C_NewCommentsUpdated;

                start = DateTime.Now;
                while (start.AddMinutes(1) > DateTime.Now) { }

                post.Comments.MonitorNew();  // Toggle off.
                post.Comments.NewUpdated -= C_NewCommentsUpdated;

                Console.WriteLine("Done monitoring!");

                // Now let's monitor r/all for a bit.  --Kris
                Subreddit all = reddit.Subreddit("all");
                all.Posts.GetNew();

                Console.WriteLine("Monitoring r/all for new posts....");

                all.Posts.MonitorNew();  // Toggle on.
                all.Posts.NewUpdated += C_NewPostsUpdated;

                start = DateTime.Now;
                while (start.AddMinutes(1) > DateTime.Now) { }

                all.Posts.MonitorNew();  // Toggle off.
                all.Posts.NewUpdated -= C_NewPostsUpdated;

                Console.WriteLine("Done monitoring!");
            }
        }

        /// <summary>
        /// Custom event handler for handling monitored new posts as they come in.
        /// See Reddit.NETTests for more complex examples.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void C_NewPostsUpdated(object sender, PostsUpdateEventArgs e)
        {
            foreach (Post post in e.Added)
            {
                Console.WriteLine("[" + post.Subreddit + "] New Post by " + post.Author + ": " + post.Title);
            }
        }

        /// <summary>
        /// Custom event handler for handling monitored new comments as they come in.
        /// See Reddit.NETTests for more complex examples.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void C_NewCommentsUpdated(object sender, CommentsUpdateEventArgs e)
        {
            foreach (Comment comment in e.Added)
            {
                Console.WriteLine("[" + comment.Subreddit + "/" + comment.Root.Title + "] New Comment by " + comment.Author + ": " + comment.Body);
            }
        }
    }
}
