using System;

namespace Reddit.NET.Controllers
{
    public class SelfPost : Post
    {
        public string SelfText;
        public string SelfTextHTML;

        /// <summary>
        /// Create new Self Post instance from Reddit API listing.
        /// </summary>
        /// <param name="listing">Listing returned by Reddit API.</param>
        public SelfPost(Dispatch dispatch, Models.Structures.Post listing) : base(dispatch, listing)
        {
            this.SelfText = listing.SelfText;
            this.SelfTextHTML = listing.SelfTextHTML;
        }

        /// <summary>
        /// Create a new Self Post instance and populate manually.
        /// </summary>
        /// <param name="subreddit">The subreddit the post belongs to.</param>
        /// <param name="title">Post title.</param>
        /// <param name="author">Reddit user who authored the post.</param>
        /// <param name="selfText">The post body.</param>
        /// <param name="selfTextHtml">The HTML-formateed post body.</param>
        /// <param name="id">Post ID.</param>
        /// <param name="name">Post name.</param>
        /// <param name="permalink">Permalink of post.</param>
        /// <param name="created">When the post was created.</param>
        /// <param name="edited">When the post was last edited.</param>
        /// <param name="score">Net vote score.</param>
        /// <param name="upVotes">Number of upvotes.</param>
        /// <param name="downVotes">Number of downvotes.</param>
        /// <param name="removed">Whether the post was removed.</param>
        /// <param name="spam">Whether the post was marked as spam.</param>
        public SelfPost(Dispatch dispatch, string subreddit, string title, string author, string selfText, string selfTextHtml,
            string id = null, string name = null, string permalink = null, DateTime created = default(DateTime),
            DateTime edited = default(DateTime), int score = 0, int upVotes = 0, int downVotes = 0,
            bool removed = false, bool spam = false)
            : base(dispatch, subreddit, title, author, id, name, permalink, created, edited, score, upVotes, downVotes,
                  removed, spam)
        {
            this.SelfText = selfText;
            this.SelfTextHTML = selfTextHtml;

            this.Listing = new Models.Structures.Post(this);
        }

        /// <summary>
        /// Query the Reddit API and populate this new instance with the result.
        /// </summary>
        /// <param name="postId">The Reddit post ID.</param>
        public SelfPost(Dispatch dispatch, string subreddit, string postId) : base(dispatch)
        {
            GetByPostId(subreddit, postId);

            this.Listing = new Models.Structures.Post(this);
        }

        /// <summary>
        /// Create an empty SelfPost instance.
        /// </summary>
        public SelfPost(Dispatch dispatch) : base(dispatch) { }

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

        /// <summary>
        /// Query the Reddit API and populate this instance with the result.
        /// </summary>
        /// <param name="subreddit">The subreddit where the post exists.</param>
        /// <param name="postId">The Reddit post ID.</param>
        private void GetByPostId(string subreddit, string postId)
        {
            // TODO
        }
    }
}
