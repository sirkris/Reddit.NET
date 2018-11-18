using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Controllers
{
    /// <summary>
    /// Base class for posts and comments.
    /// </summary>
    public abstract class Post : BaseController
    {
        public string Subreddit;
        public string Title;
        public string Author;
        public string Id;
        public string Fullname;
        public string Permalink;
        public DateTime Created;
        public DateTime Edited;
        public int Score;
        public int UpVotes;
        public int DownVotes;
        public bool Removed;
        public bool Spam;
        public bool NSFW;

        /// <summary>
        /// The full Listing object returned by the Reddit API;
        /// </summary>
        public RedditThings.Post Listing;

        public List<Comment> Comments;  // TODO - Populate.  --Kris

        internal readonly Dispatch Dispatch;

        public Post(Dispatch dispatch, RedditThings.Post listing)
        {
            Subreddit = listing.Subreddit;
            Title = listing.Title;
            Author = listing.Author;
            Id = listing.Id;
            Fullname = listing.Name;
            Permalink = listing.Permalink;
            Created = listing.Created;
            Edited = listing.Edited;
            Score = listing.Score;
            UpVotes = listing.Ups;
            DownVotes = listing.Downs;
            Removed = listing.Removed;
            Spam = listing.Spam;
            NSFW = listing.Over18;

            Listing = listing;

            Dispatch = dispatch;
        }

        public Post(Dispatch dispatch, string subreddit, string title, string author, string id = null, string name = null, string permalink = null,
            DateTime created = default(DateTime), DateTime edited = default(DateTime), int score = 0, int upVotes = 0,
            int downVotes = 0, bool removed = false, bool spam = false, bool nsfw = false)
        {
            Subreddit = subreddit;
            Title = title;
            Author = author;
            Id = id;
            Fullname = name;
            Permalink = permalink;
            Created = created;
            Edited = edited;
            Score = score;
            UpVotes = upVotes;
            DownVotes = downVotes;
            Removed = removed;
            Spam = spam;
            NSFW = nsfw;

            Listing = new RedditThings.Post(this);

            Dispatch = dispatch;
        }

        public Post(Dispatch dispatch)
        {
            Dispatch = dispatch;
        }

        abstract public RedditThings.PostResultShortData Submit(bool resubmit = false, bool ad = false, string app = "", string extension = "",
            string flairId = "", string flairText = "", string gRecapthaResponse = "", bool sendReplies = true, bool spoiler = false,
            string videoPosterUrl = "");
    }
}
