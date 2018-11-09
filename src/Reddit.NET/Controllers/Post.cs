using ModelStructures = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Controllers
{
    /// <summary>
    /// Base class for posts and comments.
    /// </summary>
    public abstract class Post
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
        public ModelStructures.Post Listing;

        public List<Comment> Comments;  // TODO - Populate.  --Kris

        private readonly Dispatch Dispatch;

        public Post(Dispatch dispatch, ModelStructures.Post listing)
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

            this.Dispatch = dispatch;
        }

        public Post(Dispatch dispatch, string subreddit, string title, string author, string id = null, string name = null, string permalink = null,
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

            this.Listing = new ModelStructures.Post(this);

            this.Dispatch = dispatch;
        }

        public Post(Dispatch dispatch)
        {
            this.Dispatch = dispatch;
        }

        abstract public bool Submit();
        abstract public bool Validate();
    }
}
