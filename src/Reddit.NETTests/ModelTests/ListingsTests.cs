using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Exceptions;
using Reddit.Models.Structures;
using System.Collections.Generic;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class ListingsTests : BaseTests
    {
        public ListingsTests() : base() { }

        [TestMethod]
        public void Best()
        {
            PostContainer posts = reddit.Models.Listings.Best(null, null, true);

            Validate(posts);
        }

        [TestMethod]
        public void BestNoCats()
        {
            PostContainer posts = reddit.Models.Listings.Best(null, null, false);

            Validate(posts);
        }

        [TestMethod]
        public void BestWithSrDetail()
        {
            PostContainer posts = reddit.Models.Listings.Best(null, null, true, 0, 25, "all", true);

            Validate(posts);
            Assert.IsNotNull(posts.Data.Children[0].Data.SrDetail);
        }

        [TestMethod]
        public void GetByNames()
        {
            PostContainer posts = reddit.Models.Listings.GetByNames("t3_9gaze5,t3_9mfizx");

            Validate(posts);
        }

        [TestMethod]
        public void GetComments()
        {
            List<(PostContainer, CommentContainer)> res = reddit.Models.Listings.GetComments("9gaze5", 0, false, false, "top", true, 0);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count == 1);

            PostContainer post = res[0].Item1;
            CommentContainer comments = res[0].Item2;

            Validate(post);
            Assert.IsTrue(post.Data.Children[0].Data.Approved);
            Assert.IsFalse(post.Data.Children[0].Data.IsSelf);
            Assert.IsTrue(post.Data.Children[0].Data.Id.Equals("9gaze5"));
            Assert.IsTrue(post.Data.Children[0].Data.Author.Equals("KrisCraig"));
            Assert.IsTrue(post.Data.Children[0].Data.Subreddit.Equals("StillSandersForPres"));
            Assert.IsTrue(post.Data.Children[0].Data.Title.Equals("Reports of Widespread Voter Suppression in New York State Democratic Primary"));

            Validate(comments);
        }

        [TestMethod]
        public void GetCommentsWithEditsAndMoreAndTruncate()
        {
            List<(PostContainer, CommentContainer)> res = reddit.Models.Listings.GetComments("8gmf99", 0, true, true, "top", true, 50);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count == 1);

            PostContainer post = res[0].Item1;
            CommentContainer comments = res[0].Item2;

            Validate(post);
            Assert.IsFalse(post.Data.Children[0].Data.IsSelf);
            Assert.IsTrue(post.Data.Children[0].Data.Id.Equals("8gmf99"));
            Assert.IsTrue(post.Data.Children[0].Data.Author.Equals("KrisCraig"));
            Assert.IsTrue(post.Data.Children[0].Data.Subreddit.Equals("FloridaMan"));
            Assert.IsTrue(post.Data.Children[0].Data.Title.Equals("Florida man with handlebar mustache assaults woman on plane, starts a fight with several passengers, " +
                "yells at police to tase him and \"you'll see what happens\", then gets tased 10 times."));

            Validate(comments);
        }

        [TestMethod]
        public void GetCommentsWithContext()
        {
            List<(PostContainer, CommentContainer)> res = reddit.Models.Listings.GetComments("8gmf99", 8, true, true, "top", true, 0, "FloridaMan", "dyd2vtc");

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count == 1);

            PostContainer post = res[0].Item1;
            CommentContainer comments = res[0].Item2;

            Validate(post);
            Assert.IsFalse(post.Data.Children[0].Data.IsSelf);
            Assert.IsTrue(post.Data.Children[0].Data.Id.Equals("8gmf99"));
            Assert.IsTrue(post.Data.Children[0].Data.Author.Equals("KrisCraig"));
            Assert.IsTrue(post.Data.Children[0].Data.Subreddit.Equals("FloridaMan"));
            Assert.IsTrue(post.Data.Children[0].Data.Title.Equals("Florida man with handlebar mustache assaults woman on plane, starts a fight with several passengers, " +
                "yells at police to tase him and \"you'll see what happens\", then gets tased 10 times."));

            Validate(comments);
        }

        [TestMethod]
        public void Hot()
        {
            PostContainer posts = reddit.Models.Listings.Hot("GLOBAL", null, null, true);

            Validate(posts);
        }

        [TestMethod]
        public void New()
        {
            PostContainer posts = reddit.Models.Listings.New(null, null, true, "StillSandersForPres");

            Validate(posts);
        }

        [TestMethod]
        public void Random()
        {
            // If there's a problem, Random() can pass on some and fail on others, so we'll do a short loop to better catch that on a single run.  --Kris
            for (int i = 1; i <= 5; i++)
            {
                List<PostContainer> posts = reddit.Models.Listings.Random("facepalm");

                Assert.IsNotNull(posts);
                Assert.IsTrue(posts.Count > 0);
                Validate(posts[0]);
            }
        }

        [TestMethod]
        public void RandomNoSub()
        {
            // If there's a problem, Random() can pass on some and fail on others, so we'll do a short loop to better catch that on a single run.  --Kris
            for (int i = 1; i <= 5; i++)
            {
                List<PostContainer> posts;
                try
                {
                    posts = reddit.Models.Listings.Random();
                }
                catch (RedditForbiddenException)
                {
                    continue;
                }

                Assert.IsNotNull(posts);
                Assert.IsTrue(posts.Count > 0);
                Validate(posts[0]);
            }
        }

        [TestMethod]
        public void Rising()
        {
            PostContainer posts = reddit.Models.Listings.Rising(null, null, true);

            Validate(posts);
        }

        [TestMethod]
        public void Top()
        {
            PostContainer posts = reddit.Models.Listings.Top("all", null, null, true);

            Validate(posts);
        }

        [TestMethod]
        public void TopDay()
        {
            PostContainer posts = reddit.Models.Listings.Top("day", null, null, true);

            Validate(posts);
        }

        [TestMethod]
        public void Controversial()
        {
            PostContainer posts = reddit.Models.Listings.Controversial("all", null, null, true);

            Validate(posts);
        }

        [TestMethod]
        public void GetDuplicates()
        {
            List<PostContainer> posts = reddit.Models.Listings.GetDuplicates("9gaze5", "", "", false, "num_comments", "");

            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count > 0);
            Validate(posts[0]);
        }
    }
}
