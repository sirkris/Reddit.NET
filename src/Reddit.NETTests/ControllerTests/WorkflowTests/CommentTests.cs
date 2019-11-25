using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void RepliesTree()
        {
            Validate(Comment.Reply("Depth = 1").Reply("Depth = 2").Reply("Depth = 3").Reply("Depth = 4").Reply("Depth = 5").Reply("Depth = 6").Reply("Depth = 7").Reply("Depth = 8")
                .Reply("Depth = 9").Reply("Depth = 10").Reply("Depth = 11").Reply("Depth = 12").Reply("Depth = 13").Reply("Depth = 14").Reply("Depth = 15").Reply("Depth = 16")
                .Reply("Depth = 17").Reply("Depth = 18").Reply("Depth = 19").Reply("Depth = 20"));

            Thread.Sleep(10000);

            Comment = Comment.About();
            Assert.IsNotNull(Comment.Replies);
            Assert.IsFalse(Comment.Replies.Count.Equals(0));

            bool[] replies = new bool[20];
            CheckReplies(Comment, ref replies);

            Assert.IsFalse(replies.Contains(false));
        }

        [TestMethod]
        public void RandomTree()
        {
            BuildRandomTree(out Queue<Comment> randomTree);
            ValidateRandomTree(ref randomTree);
        }

        [TestMethod]
        public void MoreReplies()
        {
            LinkPost post = reddit.Subreddit("news").LinkPost("t3_2lt3d0").About();
            Assert.IsNotNull(post);

            Assert.IsNotNull(post.Comments.Confidence);
            Assert.IsFalse(post.Comments.Confidence.Count.Equals(0));
            Assert.AreEqual(post.Comments.Confidence[0].Body, "I have no idea whats going on");
            Assert.AreEqual(post.Comments.Confidence[0].Id, "cly40cf");
            Assert.IsNotNull(post.Comments.Confidence[0].Replies);
            Assert.IsFalse(post.Comments.Confidence[0].Replies.Count.Equals(0));
            Assert.AreEqual(post.Comments.Confidence[0].Replies[0].Id, "cly6q06");
            Assert.IsNotNull(post.Comments.Confidence[0].Replies[0].Replies);
            Assert.IsFalse(post.Comments.Confidence[0].Replies[0].Replies.Count.Equals(0));
            Assert.AreEqual(post.Comments.Confidence[0].Replies[0].Replies[0].Id, "clyaevo");

            Assert.IsNotNull(post.Comments.Confidence[0].Replies[0].Replies[0].More);
            Assert.IsFalse(post.Comments.Confidence[0].Replies[0].Replies[0].More.Count.Equals(0));
            Assert.IsNotNull(post.Comments.Confidence[0].Replies[0].Replies[0].More[0].Children);
            Assert.IsFalse(post.Comments.Confidence[0].Replies[0].Replies[0].More[0].Children.Count.Equals(0));
            Assert.IsNotNull(post.Comments.Confidence[0].Replies[0].Replies[0].Replies);
            Assert.IsFalse(post.Comments.Confidence[0].Replies[0].Replies[0].Replies.Count.Equals(0));

            // Now make sure the more entries correspond to actual replies.  --Kris
            HashSet<string> ids = new HashSet<string>();
            foreach (Comment comment in post.Comments.Confidence[0].Replies[0].Replies[0].Replies)
            {
                ids.Add(comment.Id);
            }

            foreach (string childId in post.Comments.Confidence[0].Replies[0].Replies[0].More[0].Children)
            {
                Assert.IsTrue(ids.Contains(childId));
            }
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

        private void CheckReplies(Comment comment, ref bool[] replies, int depth = 0)
        {
            if (depth < 20 && comment.Replies != null && !comment.Replies.Count.Equals(0))
            {
                replies[depth] = true;
                CheckReplies(comment.Replies[0], ref replies, (depth + 1));
            }
        }

        private void BuildRandomTree(out Queue<Comment> comments, int minBase = 2, int maxBase = 4, int minDepth = 1, int maxDepth = 11)
        {
            comments = new Queue<Comment>();  // The comments, in order, regardless of depth.  For later comparison.  --Kris
            for (int i = 1; i <= Random.Next(minBase, maxBase); i++)
            {
                comments.Enqueue(Post.Reply("Comment #" + i.ToString()));

                Stack<int> indexes = new Stack<int>();
                indexes.Push(i);

                int baseReplies = Random.Next(minBase, maxBase);
                int j = 0;
                Comment comment = comments.Last();
                while (maxDepth > 0 && minDepth > 0 && maxDepth > minDepth 
                    && j < baseReplies)
                {
                    j++;

                    BuildReplyTree(comment, ref comments, indexes, Random.Next(minDepth, maxDepth), index: j);
                }

                indexes.Pop();
            }
        }

        private Comment BuildReplyTree(Comment comment, ref Queue<Comment> comments, Stack<int> indexes, int maxDepth, int curDepth = 0, int index = 1)
        {
            indexes.Push(index);
            comment = BuildReply(comment, ref comments, indexes, maxDepth, curDepth);
            indexes.Pop();

            return comment;
        }

        private Comment BuildReply(Comment comment, ref Queue<Comment> comments, Stack<int> indexes, int maxDepth, int curDepth = 0)
        {
            comment = comment.Reply(BuildReplyBody(indexes));
            EnqueueComment(comment, ref comments);

            return (curDepth < maxDepth
                ? BuildReplyTree(comment, ref comments, indexes, maxDepth, (curDepth + 1))
                : comment);
        }

        private string BuildReplyBody(Stack<int> indexes)
        {
            string res = "Reply #";
            int i = 0;
            foreach (int index in indexes.Reverse())
            {
                if (i > 0)
                {
                    res += ".";
                }

                res += index.ToString();
                i++;
            }

            return res;
        }

        private void EnqueueComment(Comment comment, ref Queue<Comment> comments)
        {
            comments.Enqueue(comment);
            if (comment.Replies != null)
            {
                foreach (Comment reply in comment.Replies)
                {
                    EnqueueComment(reply, ref comments);
                }
            }
        }

        private void ValidateRandomTree(ref Queue<Comment> comments)
        {
            foreach (Comment comment in Post.Comments.Old)
            {
                ValidateRandomTree(ref comments, comment);
            }
        }

        private void ValidateRandomTree(ref Queue<Comment> comments, Comment comment)
        {
            if (comments != null && !comments.Count.Equals(0))
            {
                Assert.AreEqual(comments.Dequeue().Id, comment.Id);
                if (comment.Replies != null)
                {
                    foreach (Comment reply in comment.Replies)
                    {
                        ValidateRandomTree(ref comments, reply);
                    }
                }
            }
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
