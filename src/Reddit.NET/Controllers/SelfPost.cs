using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Controllers
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

        /// <summary>
        /// Submit self post to Reddit.
        /// </summary>
        /// <returns>Whether submission was successful.</returns>
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
            return (!String.IsNullOrWhiteSpace(Subreddit)
                && !String.IsNullOrWhiteSpace(Title));
        }
    }
}
