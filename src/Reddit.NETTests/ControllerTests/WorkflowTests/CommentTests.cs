using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Controllers;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.NETTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class CommentTests : BaseTests
    {
        private SelfPost Post
        {
            get
            {
                return post ?? TestSelfPost();
            }
            set
            {
                post = value;
            }
        }
        private SelfPost post;

        private Comment Comment
        {
            get
            {
                return comment ?? TestComment();
            }
            set
            {
                comment = value;
            }
        }
        private Comment comment;
        
        public CommentTests() : base() { }

        private SelfPost TestSelfPost()
        {
            Post = reddit.Subreddit(testData["Subreddit"]).SelfPost("Test Self Post (Now With Comments!)", "It is now: " + DateTime.Now.ToString("F")).Submit();
            return Post;
        }

        private Comment TestComment()
        {
            Comment = Post.Comment("This is a test comment.").Submit();
            return Comment;
        }

        [TestMethod]
        public void Submit()
        {
            Validate(Comment);
        }

        [TestMethod]
        public void Delete()
        {
            Comment.Delete();
        }

        [TestMethod]
        public void Report()
        {
            Comment.Report("This is a test (additional info).", "", "This is a test (custom).", false, "This is a test (other).", "This is a test (reason).",
                "This is a test (rule reason).", "This is a test (site reason).", "");
        }

        [TestMethod]
        public void Edit()
        {
            Validate(Comment.Edit("This comment has been edited."));
        }

        [TestMethod]
        public async Task EditAsync()
        {
            await Comment.EditAsync("This comment has been edited asynchronously.");
        }

        [TestMethod]
        public async Task ModifyAsync()
        {
            await Comment.SubmitAsync();
            await Comment.ReportAsync("This is a test (additional info).", "", "This is a test (custom).", false, "This is a test (other).", "This is a test (reason).",
                "This is a test (rule reason).", "This is a test (site reason).", "");
            await Comment.SaveAsync("RDNTestCat");
            await Comment.UnsaveAsync();
            await Comment.DisableSendRepliesAsync();
            await Comment.EnableSendRepliesAsync();
        }
    }
}
