using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using Things = Reddit.Things;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RedditTests.ControllerTests.WorkflowTests
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

        private Dictionary<string, Comment> NewComments;
        private Random Random;

        public CommentTests() : base()
        {
            NewComments = new Dictionary<string, Comment>();
            Random = new Random();
        }

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
        public void Distinguish()
        {
            Comment.Distinguish("yes");
        }

        [TestMethod]
        public void Delete()
        {
            Comment.Delete();
        }

        [TestMethod]
        public async Task DeleteAsync()
        {
            await Comment.DeleteAsync();
        }

        [TestMethod]
        public void Report()
        {
            Comment.Report("This is a test (additional info).", "", "This is a test (custom).", false, "This is a test (other).", "This is a test (reason).",
                "This is a test (rule reason).", "This is a test (site reason).", "");
        }

        [TestMethod]
        public async Task ReportAsync()
        {
            await Comment.ReportAsync("This is a test (additional info).", "", "This is a test (custom).", false, "This is a test (other).", "This is a test (reason).",
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
        public void Reply()
        {
            Validate(Comment.Reply("This is a comment reply.").Reply("This is another reply.").Reply("This is yet another reply."));
        }

        [TestMethod]
        public void RepliesWithMore()
        {
            Validate(Comment.Reply("Depth = 1").Reply("Depth = 2").Reply("Depth = 3").Reply("Depth = 4").Reply("Depth = 5").Reply("Depth = 6").Reply("Depth = 7").Reply("Depth = 8")
                .Reply("Depth = 9").Reply("Depth = 10").Reply("Depth = 11").Reply("Depth = 12").Reply("Depth = 13").Reply("Depth = 14").Reply("Depth = 15").Reply("Depth = 16")
                .Reply("Depth = 17").Reply("Depth = 18").Reply("Depth = 19").Reply("Depth = 20"));

            Thread.Sleep(10000);

            Comment = Comment.About();

            Assert.IsNotNull(Comment.Replies);
            Assert.IsFalse(Comment.Replies.Count.Equals(0));
            Assert.IsTrue(CheckReply(comment.Replies[0]));
        }

        [TestMethod]
        public void RandomTree()
        {
            BuildRandomTree(out Queue<Comment> randomTree);

            // TODO - Validate
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

        [TestMethod]
        public void Remove()
        {
            Comment.Remove();
        }

        [TestMethod]
        public void MonitorNewComments()
        {
            Comment.Comments.GetNew();  // This call prevents any existing "new"-sorted comments from triggering the update event.  --Kris
            Comment.Comments.MonitorNew();
            Comment.Comments.NewUpdated += C_NewCommentsUpdated;

            for (int i = 1; i <= 10; i++)
            {
                // Despite what VS says, we don't want to use await here.  --Kris
                Comment.ReplyAsync("Comment #" + i.ToString());
            }

            DateTime start = DateTime.Now;
            while (NewComments.Count < 10
                && start.AddMinutes(1) > DateTime.Now) { }

            Comment.Comments.NewUpdated -= C_NewCommentsUpdated;
            Comment.Comments.MonitorNew();

            Assert.IsTrue(NewComments.Count >= 7);
        }

        // TODO - MoreChildren is for base-level comments expansion, not depth!  --Kris
        private bool CheckReply(Comment comment, int depth = 1, int maxDepth = 20)
        {
            if (comment.More != null && !comment.More.Count.Equals(0))
            {
                List<Things.MoreChildren> moreChildren = new List<Things.MoreChildren>();
                foreach (Things.More more in comment.More)
                {
                    moreChildren.Add(comment.MoreChildren(string.Join(",", more.Children), true, "new", more.Id));
                }
                comment = comment.About();
            }

            return (comment != null && comment.Body.Equals("Depth = " + depth.ToString())
                ? (depth < maxDepth 
                    ? CheckReply((comment.Replies != null && !comment.Replies.Count.Equals(0) ? comment.Replies[0] : null), (depth + 1), maxDepth)
                    : true)
                : false);
        }

        private void BuildRandomTree(out Queue<Comment> comments, int minBase = 10, int maxBase = 11, int minDepth = 1, int maxDepth = 11)
        {
            comments = new Queue<Comment>();  // The comments, in order, regardless of depth.  For later comparison.  --Kris
            for (int i = 1; i <= Random.Next(minBase, maxBase); i++)
            {
                comments.Enqueue(Post.Reply("Comment #" + i.ToString()));
                if (maxDepth > 0 && minDepth > 0 && maxDepth > minDepth)
                {
                    BuildReplyTree(comment, ref comments, new List<int> { i }, Random.Next(minDepth, maxDepth)).Submit();
                }
            }
        }

        private Comment BuildReplyTree(Comment comment, ref Queue<Comment> comments, IList<int> indexes, int maxDepth, int curDepth = 0)
        {
            comment = (curDepth < maxDepth
                ? BuildReplyTree(comment.Reply(BuildReplyBody(indexes)), ref comments, indexes, maxDepth, (curDepth + 1))
                : comment.Reply(BuildReplyBody(indexes)));

            comments.Enqueue(comment);

            return comment;
        }

        private string BuildReplyBody(IList<int> indexes)
        {
            string res = "Reply #";
            for (int i = 0; i < indexes.Count; i++)
            {
                if (i > 0)
                {
                    res += ".";
                }

                res += i.ToString();
            }

            return res;
        }

        // When a new comment is detected in MonitorNewComments, this method will add it/them to the list.  --Kris
        private void C_NewCommentsUpdated(object sender, CommentsUpdateEventArgs e)
        {
            foreach (Comment comment in e.Added)
            {
                if (!NewComments.ContainsKey(comment.Fullname))
                {
                    NewComments.Add(comment.Fullname, comment);
                }
            }
        }
    }
}
