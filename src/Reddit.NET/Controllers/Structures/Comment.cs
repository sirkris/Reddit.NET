using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Controllers.Structures
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

        public Comment() { }
    }
}
