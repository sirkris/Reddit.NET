using Newtonsoft.Json.Linq;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Controllers
{
    class LinkPost : Post
    {
        public JObject Preview;
        public string URL;
        public string Thumbnail;
        public int? ThumbnailHeight;
        public int? ThumbnailWidth;

        public LinkPost(Listing listing) : base(listing)
        {
            this.Preview = listing.Preview;
            this.URL = listing.URL;
            this.Thumbnail = listing.Thumbnail;
            this.ThumbnailHeight = listing.ThumbnailHeight;
            this.ThumbnailWidth = listing.ThumbnailWidth;
        }

        public LinkPost(string subreddit, string title, string author, string url, string thumbnail = null,
            int? thumbnailHeight = null, int? thumbnailWidth = null, JObject preview = null,
            string id = null, string name = null, string permalink = null, DateTime created = default(DateTime),
            DateTime edited = default(DateTime), int score = 0, int upVotes = 0, int downVotes = 0,
            bool removed = false, bool spam = false)
            : base(subreddit, title, author, id, name, permalink, created, edited, score, upVotes, downVotes,
                  removed, spam)
        {
            this.Preview = preview;
            this.URL = url;
            this.Thumbnail = thumbnail;
            this.ThumbnailHeight = thumbnailHeight;
            this.ThumbnailWidth = thumbnailWidth;

            this.Listing = new Listing(this);
        }

        public LinkPost() { }

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
