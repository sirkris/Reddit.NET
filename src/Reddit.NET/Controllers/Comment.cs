using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Controllers
{
    class Comment : Post
    {
        public List<Listing> Replies;
        public string Body;
        public string BodyHTML;
        public string ParentId;
        public string CollapsedReason;
        public bool Collapsed;
        public bool IsSubmitter;
        public bool ScoreHidden;
        public int Depth;

        public Comment(Listing listing) : base(listing)
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

        public Comment(string subreddit, string title, string author, string body, string bodyHtml,
            string parentId = null, string collapsedReason = null, bool collapsed = false, bool isSubmitter = false,
            List<Listing> replies = null, bool scoreHidden = false, int depth = 0, string id = null, string name = null, 
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime), 
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
            : base(subreddit, title, author, id, name, permalink, created, edited, score, upVotes, downVotes,
                  removed, spam)
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

            this.Listing = new Listing(this);
        }

        public Comment() { }

        public override bool Submit()
        {
            if (!Validate())
            {
                return false;
            }

            // TODO - Submit to Reddit, populate listing, and update properties.  --Kris


            return true;
        }

        /// <summary>
        /// Check to see if all required properties are present for submission to Reddit.
        /// </summary>
        /// <returns>Whether this instance is ready to submit.</returns>
        public override bool Validate()
        {
            // TODO - Check required properties.  --Kris


            return true;
        }
    }
}
