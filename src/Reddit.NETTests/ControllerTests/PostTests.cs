using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;

namespace RedditTests.ControllerTests
{
    [TestClass]
    public class PostTests : BaseTests
    {
        protected string PostFullname;

        private Post Post
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
        private Post post;

        public PostTests() : base()
        {
            PostFullname = PostFullname ?? "t3_24g5xq";
        }

        private Post GetPost()
        {
            Post = reddit.Post(PostFullname).About();
            return Post;
        }

        [TestMethod]
        public void About()
        {
            Validate(Post);
            Assert.IsTrue(Post.Fullname.Equals(PostFullname));
            Assert.IsFalse(Post.UpvoteRatio.Equals(0));
            Assert.IsFalse(Post.DownVotes.Equals(0));
        }

        [TestMethod]
        public void Awards()
        {
            Post post = reddit.Post("t3_dwut73").About();
            Validate(post);
            Assert.IsTrue(post.Awards.Count >= 174);
            Assert.IsTrue(post.Awards.Silver >= 159);
            Assert.IsTrue(post.Awards.Gold >= 11);
            Assert.IsTrue(post.Awards.Platinum >= 4);
        }

        [TestMethod]
        public void Hide()
        {
            Post.Hide();
        }

        [TestMethod]
        public void Unhide()
        {
            Post.Unhide();
        }

        [TestMethod]
        public virtual void MoreChildren()
        {
            Reddit.Things.MoreChildren moreChildren = Post.MoreChildren("ch6sgn4", false, "new");

            Validate(moreChildren);
            Assert.IsTrue(moreChildren.Comments.Count > 0);
        }

        [TestMethod]
        public void Save()
        {
            Post.Save("RDNTestCat");
        }

        [TestMethod]
        public void Unsave()
        {
            Post.Unsave();
        }

        [TestMethod]
        public void EnableSendReplies()
        {
            Post.EnableSendReplies();
        }

        [TestMethod]
        public void DisableSendReplies()
        {
            Post.DisableSendReplies();
        }

        [TestMethod]
        public void ConfidenceReplies()
        {
            Validate(Post.Comments.Confidence);
            Assert.IsTrue(Post.Comments.Confidence.Count > 0);
        }

        [TestMethod]
        public void IConfidenceReplies()
        {
            Validate(Post.Comments.IConfidence);
            Assert.IsTrue(Post.Comments.IConfidence.Count > 0);
        }

        [TestMethod]
        public void TopReplies()
        {
            Validate(Post.Comments.Top);
            Assert.IsTrue(Post.Comments.Top.Count > 0);
        }

        [TestMethod]
        public void ITopReplies()
        {
            Validate(Post.Comments.ITop);
            Assert.IsTrue(Post.Comments.ITop.Count > 0);
        }

        [TestMethod]
        public void NewReplies()
        {
            Validate(Post.Comments.New);
            Assert.IsTrue(Post.Comments.New.Count > 0);
        }

        [TestMethod]
        public void INewReplies()
        {
            Validate(Post.Comments.INew);
            Assert.IsTrue(Post.Comments.INew.Count > 0);
        }

        [TestMethod]
        public void ControversialReplies()
        {
            Validate(Post.Comments.Controversial);
            Assert.IsTrue(Post.Comments.Controversial.Count > 0);
        }

        [TestMethod]
        public void IControversialReplies()
        {
            Validate(Post.Comments.IControversial);
            Assert.IsTrue(Post.Comments.IControversial.Count > 0);
        }

        [TestMethod]
        public void OldReplies()
        {
            Validate(Post.Comments.Old);
            Assert.IsTrue(Post.Comments.Old.Count > 0);
        }

        [TestMethod]
        public void IOldReplies()
        {
            Validate(Post.Comments.IOld);
            Assert.IsTrue(Post.Comments.IOld.Count > 0);
        }

        [TestMethod]
        public void RandomReplies()
        {
            Validate(Post.Comments.Random);
            Assert.IsTrue(Post.Comments.Random.Count > 0);
        }

        [TestMethod]
        public void IRandomReplies()
        {
            Validate(Post.Comments.IRandom);
            Assert.IsTrue(Post.Comments.IRandom.Count > 0);
        }

        [TestMethod]
        public void QAReplies()
        {
            Validate(Post.Comments.QA);
            Assert.IsTrue(Post.Comments.QA.Count > 0);
        }

        [TestMethod]
        public void IQAReplies()
        {
            Validate(Post.Comments.IQA);
            Assert.IsTrue(Post.Comments.IQA.Count > 0);
        }

        [TestMethod]
        public void LiveReplies()
        {
            Validate(Post.Comments.Live);
            Assert.IsTrue(Post.Comments.Live.Count > 0);
        }

        [TestMethod]
        public void ILiveReplies()
        {
            Validate(Post.Comments.ILive);
            Assert.IsTrue(Post.Comments.ILive.Count > 0);
        }
    }
}
