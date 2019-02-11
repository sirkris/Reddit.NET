using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;
using Reddit.Things;

namespace RedditTests.ControllerTests
{
    [TestClass]
    public class SelfPostTests : PostTests
    {
        private SelfPost Post
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
        private SelfPost post;

        public SelfPostTests() : base()
        {
            PostFullname = "t3_9hng9v";
        }

        private SelfPost GetPost()
        {
            Post = reddit.SelfPost(PostFullname).About();
            return Post;
        }

        [TestMethod]
        public void IsSelf()
        {
            Assert.IsTrue(Post.Listing.IsSelf);
            Assert.IsNotNull(Post.SelfText);
        }

        [TestMethod]
        public override void MoreChildren()
        {
            MoreChildren moreChildren = Post.MoreChildren("e6doo53", false, "new");

            Validate(moreChildren);
            Assert.IsTrue(moreChildren.Comments.Count > 0);
        }
    }
}
