using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Controllers
{
    /// <summary>
    /// Base class for posts and comments.
    /// </summary>
    abstract class Post
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

        public Post(string subreddit, string title, string author, string id = null, string name = null, string permalink = null,
            DateTime created = default(DateTime), DateTime edited = default(DateTime), int score = 0, int upVotes = 0,
            int downVotes = 0, bool removed = false, bool spam = false)
        {
            this.Subreddit = subreddit;
            this.Title = title;
            this.Author = author;
            this.Id = id;
            this.Name = name;
            this.Permalink = permalink;
            this.Created = created;
            this.Edited = edited;
            this.Score = score;
            this.UpVotes = upVotes;
            this.DownVotes = downVotes;
            this.Removed = removed;
            this.Spam = spam;

            this.Listing = new Listing(this);
        }

        public Post() { }

        abstract public bool Submit();
        abstract public bool Validate();
    }
}
