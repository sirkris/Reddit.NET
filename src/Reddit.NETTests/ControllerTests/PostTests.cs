using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Controllers;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ControllerTests
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
            RedditThings.MoreChildren moreChildren = Post.MoreChildren("ch6sgn4", false, "new");

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
        public void TopReplies()
        {
            Validate(Post.Comments.Top);
            Assert.IsTrue(Post.Comments.Top.Count > 0);
        }

        [TestMethod]
        public void NewReplies()
        {
            Validate(Post.Comments.New);
            Assert.IsTrue(Post.Comments.New.Count > 0);
        }

        [TestMethod]
        public void ControversialReplies()
        {
            Validate(Post.Comments.Controversial);
            Assert.IsTrue(Post.Comments.Controversial.Count > 0);
        }

        [TestMethod]
        public void OldReplies()
        {
            Validate(Post.Comments.Old);
            Assert.IsTrue(Post.Comments.Old.Count > 0);
        }

        [TestMethod]
        public void RandomReplies()
        {
            Validate(Post.Comments.Random);
            Assert.IsTrue(Post.Comments.Random.Count > 0);
        }

        [TestMethod]
        public void QAReplies()
        {
            Validate(Post.Comments.QA);
            Assert.IsTrue(Post.Comments.QA.Count > 0);
        }

        [TestMethod]
        public void LiveReplies()
        {
            Validate(Post.Comments.Live);
            Assert.IsTrue(Post.Comments.Live.Count > 0);
        }
    }
}
