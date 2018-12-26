using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;

namespace RedditTests.ControllerTests
{
    [TestClass]
    public class LinkPostTests : PostTests
    {
        private LinkPost Post
        {
            get
            {
                return post ?? GetPost();
            }
            set
            {
                post = value;
            }
        }
        private LinkPost post;

        public LinkPostTests() : base() { }

        private LinkPost GetPost()
        {
            Post = reddit.LinkPost(PostFullname).About();
            return Post;
        }

        [TestMethod]
        public void GetDuplicates()
        {
            Validate(Post.GetDuplicates());
        }

        [TestMethod]
        public void GetCrossPosts()
        {
            Validate(Post.GetCrossPosts());
        }

        [TestMethod]
        public void IsLink()
        {
            Assert.IsFalse(Post.Listing.IsSelf);
            Assert.IsNotNull(Post.URL);
        }
    }
}
