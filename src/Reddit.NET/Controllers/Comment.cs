using RedditThings = Reddit.NET.Models.Structures;
using System;

namespace Reddit.NET.Controllers
{
    public class Comment
    {
        public string Subreddit;
        public string Author;
        public string Id;
        public string Name;
        public string Permalink;
        public DateTime Created;
        public DateTime Edited;
        public int Score;
        public int UpVotes;
        public int DownVotes;
        public bool Removed;
        public bool Spam;
        public RedditThings.CommentContainer Replies;
        public string Body;
        public string BodyHTML;
        public string ParentId;
        public string CollapsedReason;
        public bool Collapsed;
        public bool IsSubmitter;
        public bool ScoreHidden;
        public int Depth;

        public RedditThings.Comment Listing;

        public Comment(Dispatch dispatch, RedditThings.Comment listing)
        {
            this.Replies = listing.Replies;
            this.Body = listing.Body;
            this.BodyHTML = listing.BodyHTML;
            this.ParentId = listing.ParentId;
            this.CollapsedReason = listing.CollapsedReason;
            this.Collapsed = listing.Collapsed;
            this.IsSubmitter = listing.IsSubmitter;
            this.ScoreHidden = listing.ScoreHidden;
            this.Depth = listing.Depth;
        }

        public Comment(Dispatch dispatch, string subreddit, string title, string author, string body, string bodyHtml,
            string parentId = null, string collapsedReason = null, bool collapsed = false, bool isSubmitter = false,
            RedditThings.CommentContainer replies = null, bool scoreHidden = false, int depth = 0, string id = null, string name = null, 
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime), 
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            this.Replies = replies;
            this.Body = body;
            this.BodyHTML = bodyHtml;
            this.ParentId = parentId;
            this.CollapsedReason = collapsedReason;
            this.Collapsed = collapsed;
            this.IsSubmitter = isSubmitter;
            this.ScoreHidden = scoreHidden;
            this.Depth = depth;

            this.Listing = new RedditThings.Comment(this);
        }

        public Comment(Dispatch dispatch) { }

        public bool Submit()
        {
            // TODO - Submit to Reddit, populate listing, and update properties.  --Kris


            return true;
        }

        /// <summary>
        /// Query the Reddit API and return an instance of this class with the result.
        /// </summary>
        /// <param name="commentId">The Reddit comment ID.</param>
        private void GetByCommentId(string commentId)
        {
            // TODO
        }
    }
}
