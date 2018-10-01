using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Controllers.Structures
{
    /// <summary>
    /// Base class for posts and comments.
    /// </summary>
    class Post
    {
        public string Subreddit;
        public string Title;
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

        /// <summary>
        /// The full Listing object returned by the Reddit API;
        /// </summary>
        public Listing Listing;

        public Post(Listing listing)
        {
            this.Subreddit = listing.Subreddit;
            this.Title = listing.Title;
            this.Author = listing.Author;
            this.Id = listing.Id;
            this.Name = listing.Name;
            this.Permalink = listing.Permalink;
            this.Created = listing.Created;
            this.Edited = listing.Edited;
            this.Score = listing.Score;
            this.UpVotes = listing.Ups;
            this.DownVotes = listing.Downs;
            this.Removed = listing.Removed;
            this.Spam = listing.Spam;

            this.Listing = listing;
        }

        public Post() { }
    }
}
