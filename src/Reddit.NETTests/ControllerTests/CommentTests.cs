using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;

namespace RedditTests.ControllerTests
{
    [TestClass]
    public class CommentTests : BaseTests
    {
        private readonly string CommentFullname;

        private Comment Comment
        {
            get
            {
                return comment ?? GetComment();
            }
            set
            {
                comment = value;
            }
        }
        private Comment comment;

        public CommentTests() : base()
        {
            CommentFullname = "t1_ch6sgn4";
        }

        private Comment GetComment()
        {
            Comment = reddit.Comment(CommentFullname).About();
            return Comment;
        }

        [TestMethod]
        public void About()
        {
            Validate(Comment);
            Assert.IsTrue(Comment.Fullname.Equals(CommentFullname));
        }

        [TestMethod]
        public void Awards()
        {
            Comment comment = reddit.Comment("t1_f7m226d").About();
            Validate(comment);
            Assert.IsTrue(comment.Awards.Count >= 37);
            Assert.IsTrue(comment.Awards.Silver >= 29);
            Assert.IsTrue(comment.Awards.Gold >= 7);
            Assert.IsTrue(comment.Awards.Platinum >= 1);
        }

        [TestMethod]
        public void Root()
        {
            Post post = Comment.Root;

            Validate(post);
            Assert.IsTrue(post.Fullname.Equals("t3_24g5xq"));
        }

        [TestMethod]
        public void Save()
        {
            Comment.Save("RDNTestCat");
        }

        [TestMethod]
        public void Unsave()
        {
            Comment.Unsave();
        }

        [TestMethod]
        public void EnableSendReplies()
        {
            Comment.EnableSendReplies();
        }

        [TestMethod]
        public void DisableSendReplies()
        {
            Comment.DisableSendReplies();
        }

        [TestMethod]
        public void MoreChildren()
        {
            Reddit.Things.MoreChildren moreChildren = Comment.MoreChildren(Comment.Id, false, "new");

            Validate(moreChildren);
            Assert.IsTrue(moreChildren.Comments.Count > 0);
        }

        [TestMethod]
        public void ConfidenceReplies()
        {
            Validate(Comment.Comments.Confidence);
            Assert.IsTrue(Comment.Comments.Confidence.Count > 0);
        }

        [TestMethod]
        public void IConfidenceReplies()
        {
            Validate(Comment.Comments.IConfidence);
            Assert.IsTrue(Comment.Comments.IConfidence.Count > 0);
        }

        [TestMethod]
        public void TopReplies()
        {
            Validate(Comment.Comments.Top);
            Assert.IsTrue(Comment.Comments.Top.Count > 0);
        }

        [TestMethod]
        public void ITopReplies()
        {
            Validate(Comment.Comments.ITop);
            Assert.IsTrue(Comment.Comments.ITop.Count > 0);
        }

        [TestMethod]
        public void NewReplies()
        {
            Validate(Comment.Comments.New);
            Assert.IsTrue(Comment.Comments.New.Count > 0);
        }

        [TestMethod]
        public void INewReplies()
        {
            Validate(Comment.Comments.INew);
            Assert.IsTrue(Comment.Comments.INew.Count > 0);
        }

        [TestMethod]
        public void ControversialReplies()
        {
            Validate(Comment.Comments.Controversial);
            Assert.IsTrue(Comment.Comments.Controversial.Count > 0);
        }

        [TestMethod]
        public void IControversialReplies()
        {
            Validate(Comment.Comments.IControversial);
            Assert.IsTrue(Comment.Comments.IControversial.Count > 0);
        }

        [TestMethod]
        public void OldReplies()
        {
            Validate(Comment.Comments.Old);
            Assert.IsTrue(Comment.Comments.Old.Count > 0);
        }

        [TestMethod]
        public void IOldReplies()
        {
            Validate(Comment.Comments.IOld);
            Assert.IsTrue(Comment.Comments.IOld.Count > 0);
        }

        [TestMethod]
        public void RandomReplies()
        {
            Validate(Comment.Comments.Random);
            Assert.IsTrue(Comment.Comments.Random.Count > 0);
        }

        [TestMethod]
        public void IRandomReplies()
        {
            Validate(Comment.Comments.IRandom);
            Assert.IsTrue(Comment.Comments.IRandom.Count > 0);
        }

        [TestMethod]
        public void QAReplies()
        {
            Validate(Comment.Comments.QA);
            Assert.IsTrue(Comment.Comments.QA.Count > 0);
        }

        [TestMethod]
        public void IQAReplies()
        {
            Validate(Comment.Comments.IQA);
            Assert.IsTrue(Comment.Comments.IQA.Count > 0);
        }

        [TestMethod]
        public void LiveReplies()
        {
            Validate(Comment.Comments.Live);
            Assert.IsTrue(Comment.Comments.Live.Count > 0);
        }

        [TestMethod]
        public void ILiveReplies()
        {
            Validate(Comment.Comments.ILive);
            Assert.IsTrue(Comment.Comments.ILive.Count > 0);
        }
    }
}
