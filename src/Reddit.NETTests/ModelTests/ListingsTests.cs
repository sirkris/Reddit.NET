using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class ListingsTests : BaseTests
    {
        [TestMethod]
        public void Best()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer posts = reddit.Models.Listings.Best(null, null, true);

            Assert.IsNotNull(posts);
            Assert.IsNotNull(posts.Data);
            Assert.IsNotNull(posts.Data.Children);
            Assert.IsTrue(posts.Data.Children.Count > 0);
            Assert.IsTrue(posts.Kind.Equals("Listing"));
            Assert.IsTrue(posts.Data.Children[0].Kind.Equals("t3"));
            Assert.IsNotNull(posts.Data.Children[0].Data);
        }

        [TestMethod]
        public void BestNoCats()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer posts = reddit.Models.Listings.Best(null, null, false);

            Assert.IsNotNull(posts);
            Assert.IsNotNull(posts.Data);
            Assert.IsNotNull(posts.Data.Children);
            Assert.IsTrue(posts.Data.Children.Count > 0);
            Assert.IsTrue(posts.Kind.Equals("Listing"));
            Assert.IsTrue(posts.Data.Children[0].Kind.Equals("t3"));
            Assert.IsNotNull(posts.Data.Children[0].Data);
        }

        [TestMethod]
        public void BestWithSrDetail()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer posts = reddit.Models.Listings.Best(null, null, true, 0, 25, "all", true);

            Assert.IsNotNull(posts);
            Assert.IsNotNull(posts.Data);
            Assert.IsNotNull(posts.Data.Children);
            Assert.IsTrue(posts.Data.Children.Count > 0);
            Assert.IsTrue(posts.Kind.Equals("Listing"));
            Assert.IsTrue(posts.Data.Children[0].Kind.Equals("t3"));
            Assert.IsNotNull(posts.Data.Children[0].Data);
            Assert.IsNotNull(posts.Data.Children[0].Data.SrDetail);
        }

        [TestMethod]
        public void GetByNames()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer posts = reddit.Models.Listings.GetByNames("t3_9gaze5,t3_9mfizx");

            Assert.IsNotNull(posts);
            Assert.IsNotNull(posts.Data);
            Assert.IsNotNull(posts.Data.Children);
            Assert.IsTrue(posts.Data.Children.Count > 0);
            Assert.IsTrue(posts.Kind.Equals("Listing"));
            Assert.IsTrue(posts.Data.Children[0].Kind.Equals("t3"));
            Assert.IsNotNull(posts.Data.Children[0].Data);
        }

        [TestMethod]
        public void GetComments()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            List<(PostContainer, CommentContainer)> res = reddit.Models.Listings.GetComments("9gaze5", 0, false, false, "top", true, 0);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count == 1);

            PostContainer post = res[0].Item1;
            CommentContainer comments = res[0].Item2;

            Assert.IsNotNull(post);
            Assert.IsNotNull(post.Data);
            Assert.IsNotNull(post.Data.Children);
            Assert.IsTrue(post.Data.Children.Count == 1);
            Assert.IsTrue(post.Kind.Equals("Listing"));
            Assert.IsTrue(post.Data.Children[0].Kind.Equals("t3"));
            Assert.IsNotNull(post.Data.Children[0].Data);
            Assert.IsTrue(post.Data.Children[0].Data.Approved);
            Assert.IsFalse(post.Data.Children[0].Data.IsSelf);
            Assert.IsTrue(post.Data.Children[0].Data.Id.Equals("9gaze5"));
            Assert.IsTrue(post.Data.Children[0].Data.Author.Equals("KrisCraig"));
            Assert.IsTrue(post.Data.Children[0].Data.Subreddit.Equals("StillSandersForPres"));
            Assert.IsTrue(post.Data.Children[0].Data.Title.Equals("Reports of Widespread Voter Suppression in New York State Democratic Primary"));

            Assert.IsNotNull(comments);
            Assert.IsNotNull(comments.Data);
            Assert.IsNotNull(comments.Data.Children);
            Assert.IsTrue(comments.Data.Children.Count > 0);
            Assert.IsTrue(comments.Kind.Equals("Listing"));
            Assert.IsTrue(comments.Data.Children[0].Kind.Equals("t1"));
            Assert.IsNotNull(comments.Data.Children[0].Data);
        }

        [TestMethod]
        public void GetCommentsWithEditsAndMoreAndTruncate()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            List<(PostContainer, CommentContainer)> res = reddit.Models.Listings.GetComments("8gmf99", 0, true, true, "top", true, 50);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count == 1);

            PostContainer post = res[0].Item1;
            CommentContainer comments = res[0].Item2;

            Assert.IsNotNull(post);
            Assert.IsNotNull(post.Data);
            Assert.IsNotNull(post.Data.Children);
            Assert.IsTrue(post.Data.Children.Count == 1);
            Assert.IsTrue(post.Kind.Equals("Listing"));
            Assert.IsTrue(post.Data.Children[0].Kind.Equals("t3"));
            Assert.IsNotNull(post.Data.Children[0].Data);
            Assert.IsFalse(post.Data.Children[0].Data.IsSelf);
            Assert.IsTrue(post.Data.Children[0].Data.Id.Equals("8gmf99"));
            Assert.IsTrue(post.Data.Children[0].Data.Author.Equals("KrisCraig"));
            Assert.IsTrue(post.Data.Children[0].Data.Subreddit.Equals("FloridaMan"));
            Assert.IsTrue(post.Data.Children[0].Data.Title.Equals("Florida man with handlebar mustache assaults woman on plane, starts a fight with several passengers, " +
                "yells at police to tase him and \"you'll see what happens\", then gets tased 10 times."));

            Assert.IsNotNull(comments);
            Assert.IsNotNull(comments.Data);
            Assert.IsNotNull(comments.Data.Children);
            Assert.IsTrue(comments.Data.Children.Count > 0);
            Assert.IsTrue(comments.Kind.Equals("Listing"));
            Assert.IsTrue(comments.Data.Children[0].Kind.Equals("t1"));
            Assert.IsNotNull(comments.Data.Children[0].Data);
        }

        [TestMethod]
        public void GetCommentsWithContext()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            List<(PostContainer, CommentContainer)> res = reddit.Models.Listings.GetComments("8gmf99", 8, true, true, "top", true, 0, "FloridaMan", "dyd2vtc");

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count == 1);

            PostContainer post = res[0].Item1;
            CommentContainer comments = res[0].Item2;

            Assert.IsNotNull(post);
            Assert.IsNotNull(post.Data);
            Assert.IsNotNull(post.Data.Children);
            Assert.IsTrue(post.Data.Children.Count == 1);
            Assert.IsTrue(post.Kind.Equals("Listing"));
            Assert.IsTrue(post.Data.Children[0].Kind.Equals("t3"));
            Assert.IsNotNull(post.Data.Children[0].Data);
            Assert.IsFalse(post.Data.Children[0].Data.IsSelf);
            Assert.IsTrue(post.Data.Children[0].Data.Id.Equals("8gmf99"));
            Assert.IsTrue(post.Data.Children[0].Data.Author.Equals("KrisCraig"));
            Assert.IsTrue(post.Data.Children[0].Data.Subreddit.Equals("FloridaMan"));
            Assert.IsTrue(post.Data.Children[0].Data.Title.Equals("Florida man with handlebar mustache assaults woman on plane, starts a fight with several passengers, " +
                "yells at police to tase him and \"you'll see what happens\", then gets tased 10 times."));

            Assert.IsNotNull(comments);
            Assert.IsNotNull(comments.Data);
            Assert.IsNotNull(comments.Data.Children);
            Assert.IsTrue(comments.Data.Children.Count > 0);
            Assert.IsTrue(comments.Kind.Equals("Listing"));
            Assert.IsTrue(comments.Data.Children[0].Kind.Equals("t1"));
            Assert.IsNotNull(comments.Data.Children[0].Data);
        }

        [TestMethod]
        public void Hot()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer posts = reddit.Models.Listings.Hot("GLOBAL", null, null, true);

            Assert.IsNotNull(posts);
            Assert.IsNotNull(posts.Data);
            Assert.IsNotNull(posts.Data.Children);
            Assert.IsTrue(posts.Data.Children.Count > 0);
            Assert.IsTrue(posts.Kind.Equals("Listing"));
            Assert.IsTrue(posts.Data.Children[0].Kind.Equals("t3"));
            Assert.IsNotNull(posts.Data.Children[0].Data);
        }

        [TestMethod]
        public void New()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer posts = reddit.Models.Listings.New(null, null, true, "StillSandersForPres");

            Assert.IsNotNull(posts);
            Assert.IsNotNull(posts.Data);
            Assert.IsNotNull(posts.Data.Children);
            Assert.IsTrue(posts.Data.Children.Count > 0);
            Assert.IsTrue(posts.Kind.Equals("Listing"));
            Assert.IsTrue(posts.Data.Children[0].Kind.Equals("t3"));
            Assert.IsNotNull(posts.Data.Children[0].Data);
        }

        [TestMethod]
        public void Random()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            // If there's a problem, Random() can pass on some and fail on others, so we'll do a short loop to better catch that on a single run.  --Kris
            for (int i = 1; i <= 5; i++)
            {
                List<PostContainer> posts = reddit.Models.Listings.Random("facepalm");

                Assert.IsNotNull(posts);
                Assert.IsTrue(posts.Count > 0);
                Assert.IsNotNull(posts[0].Data);
                Assert.IsNotNull(posts[0].Data.Children);
                Assert.IsTrue(posts[0].Data.Children.Count > 0);
                Assert.IsTrue(posts[0].Kind.Equals("Listing"));
                Assert.IsTrue(posts[0].Data.Children[0].Kind.Equals("t3"));
                Assert.IsNotNull(posts[0].Data.Children[0].Data);
            }
        }

        [TestMethod]
        public void RandomNoSub()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            // If there's a problem, Random() can pass on some and fail on others, so we'll do a short loop to better catch that on a single run.  --Kris
            for (int i = 1; i <= 5; i++)
            {
                List<PostContainer> posts = reddit.Models.Listings.Random();

                Assert.IsNotNull(posts);
                Assert.IsTrue(posts.Count > 0);
                Assert.IsNotNull(posts[0].Data);
                Assert.IsNotNull(posts[0].Data.Children);
                Assert.IsTrue(posts[0].Data.Children.Count > 0);
                Assert.IsTrue(posts[0].Kind.Equals("Listing"));
                Assert.IsTrue(posts[0].Data.Children[0].Kind.Equals("t3"));
                Assert.IsNotNull(posts[0].Data.Children[0].Data);
            }
        }

        [TestMethod]
        public void Rising()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer posts = reddit.Models.Listings.Rising(null, null, true);

            Assert.IsNotNull(posts);
            Assert.IsNotNull(posts.Data);
            Assert.IsNotNull(posts.Data.Children);
            Assert.IsTrue(posts.Data.Children.Count > 0);
            Assert.IsTrue(posts.Kind.Equals("Listing"));
            Assert.IsTrue(posts.Data.Children[0].Kind.Equals("t3"));
            Assert.IsNotNull(posts.Data.Children[0].Data);
        }

        [TestMethod]
        public void Top()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer posts = reddit.Models.Listings.Top("all", null, null, true);

            Assert.IsNotNull(posts);
            Assert.IsNotNull(posts.Data);
            Assert.IsNotNull(posts.Data.Children);
            Assert.IsTrue(posts.Data.Children.Count > 0);
            Assert.IsTrue(posts.Kind.Equals("Listing"));
            Assert.IsTrue(posts.Data.Children[0].Kind.Equals("t3"));
            Assert.IsNotNull(posts.Data.Children[0].Data);
        }

        [TestMethod]
        public void TopDay()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer posts = reddit.Models.Listings.Top("day", null, null, true);

            Assert.IsNotNull(posts);
            Assert.IsNotNull(posts.Data);
            Assert.IsNotNull(posts.Data.Children);
            Assert.IsTrue(posts.Data.Children.Count > 0);
            Assert.IsTrue(posts.Kind.Equals("Listing"));
            Assert.IsTrue(posts.Data.Children[0].Kind.Equals("t3"));
            Assert.IsNotNull(posts.Data.Children[0].Data);
        }

        [TestMethod]
        public void Controversial()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            PostContainer posts = reddit.Models.Listings.Controversial("all", null, null, true);

            Assert.IsNotNull(posts);
            Assert.IsNotNull(posts.Data);
            Assert.IsNotNull(posts.Data.Children);
            Assert.IsTrue(posts.Data.Children.Count > 0);
            Assert.IsTrue(posts.Kind.Equals("Listing"));
            Assert.IsTrue(posts.Data.Children[0].Kind.Equals("t3"));
            Assert.IsNotNull(posts.Data.Children[0].Data);
        }

        [TestMethod]
        public void GetDuplicates()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            List<PostContainer> posts = reddit.Models.Listings.GetDuplicates("9gaze5", "", "", false, "num_comments", "");

            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count > 0);
        }
    }
}
