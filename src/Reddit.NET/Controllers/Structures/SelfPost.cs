using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Controllers.Structures
{
    class SelfPost : Post
    {
        public string SelfText;
        public string SelfTextHTML;

        public SelfPost(Listing listing) : base(listing)
        {
            this.SelfText = listing.SelfText;
            this.SelfTextHTML = listing.SelfTextHTML;
        }

        public SelfPost(string subreddit, string title, string author, string selfText, string selfTextHtml,
            string id = null, string name = null, string permalink = null, DateTime created = default(DateTime),
            DateTime edited = default(DateTime), int score = 0, int upVotes = 0, int downVotes = 0,
            bool removed = false, bool spam = false)
            : base(subreddit, title, author, id, name, permalink, created, edited, score, upVotes, downVotes,
                  removed, spam)
        {
            this.SelfText = selfText;
            this.SelfTextHTML = selfTextHtml;

            this.Listing = new Listing(this);
        }

        public SelfPost() { }
    }
}
