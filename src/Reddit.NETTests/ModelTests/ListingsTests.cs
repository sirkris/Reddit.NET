using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Exceptions;
using Reddit.Inputs;
using Reddit.Inputs.Listings;
using Reddit.Things;
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
            PostContainer posts = reddit.Models.Listings.Best(new CategorizedSrListingInput(includeCategories: true));

            Validate(posts);
        }

        [TestMethod]
        public void BestNoCats()
        {
            PostContainer posts = reddit.Models.Listings.Best(new CategorizedSrListingInput(includeCategories: false));

            Validate(posts);
        }

        [TestMethod]
        public void BestWithSrDetail()
        {
            PostContainer posts = reddit.Models.Listings.Best(new CategorizedSrListingInput(srDetail: true));

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
            CommentContainer res = reddit.Models.Listings.GetComments("9gaze5", new ListingsGetCommentsInput(0, false, false, "top", true, 0));

            Assert.IsNotNull(res);
            Validate(res);
        }

        [TestMethod]
        public void GetCommentsWithEditsAndMoreAndTruncate()
        {
            CommentContainer res = reddit.Models.Listings.GetComments("8gmf99", new ListingsGetCommentsInput(0, true, true, "top", true, 50));

            Assert.IsNotNull(res);
            Validate(res);
        }

        [TestMethod]
        public void GetCommentsWithContext()
        {
            CommentContainer res = reddit.Models.Listings.GetComments("8gmf99", new ListingsGetCommentsInput(8, true, true, "top", true, 0, "dyd2vtc"), "FloridaMan");

            Assert.IsNotNull(res);
            Validate(res);
        }

        [TestMethod]
        public void Hot()
        {
            PostContainer posts = reddit.Models.Listings.Hot(new ListingsHotInput(includeCategories: true));

            Validate(posts);
        }

        [TestMethod]
        public void New()
        {
            PostContainer posts = reddit.Models.Listings.New(new CategorizedSrListingInput(includeCategories: true), "StillSandersForPres");

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
            PostContainer posts = reddit.Models.Listings.Rising(new CategorizedSrListingInput(includeCategories: true));

            Validate(posts, true);
        }

        [TestMethod]
        public void Top()
        {
            PostContainer posts = reddit.Models.Listings.Top(new TimedCatSrListingInput(includeCategories: true));

            Validate(posts);
        }

        [TestMethod]
        public void TopDay()
        {
            PostContainer posts = reddit.Models.Listings.Top(new TimedCatSrListingInput("day", includeCategories: true));

            Validate(posts);
        }

        [TestMethod]
        public void Controversial()
        {
            PostContainer posts = reddit.Models.Listings.Controversial(new TimedCatSrListingInput(includeCategories: true));

            Validate(posts);
        }

        [TestMethod]
        public void GetDuplicates()
        {
            List<PostContainer> posts = reddit.Models.Listings.GetDuplicates("9gaze5", new ListingsGetDuplicatesInput(sort: "num_comments"));

            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count > 0);
            Validate(posts[0]);
        }
    }
}
