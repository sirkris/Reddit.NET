using Reddit.NET.Controllers;
using ModelStructures = Reddit.NET.Models.Structures;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Reddit.NET
{
    public class RedditAPI
    {
        public Dispatch Models
        {
            get;
            private set;
        }

        public RedditAPI(string appId, string refreshToken, string accessToken = null)
        {
            /*
             * If refreshToken is supplied, the lib will automatically request a new access token when the current one expires (or if none was passed).
             * Otherwise it's left up to the calling app and the API calls will fail once the access token expires.
             * 
             * --Kris
             */
            if (!string.IsNullOrWhiteSpace(refreshToken)
                || !string.IsNullOrWhiteSpace(accessToken))
            {
                // Passing "null" instead of null forces the Reddit API to return a non-200 status code on auth failure, freeing us from having to parse the content string.  --Kris
                this.Models = new Dispatch(appId, refreshToken, (!string.IsNullOrWhiteSpace(accessToken) ? accessToken : "null"), new RestClient("https://oauth.reddit.com"));
            }
            else
            {
                throw new ArgumentException("Refresh token and access token can't both be empty.");
            }
        }

        public Comment Comment(ModelStructures.PostOrComment listing)
        {
            return new Comment(Models, listing);
        }

        public Comment Comment(string subreddit, string title, string author, string body, string bodyHtml,
            string parentId = null, string collapsedReason = null, bool collapsed = false, bool isSubmitter = false,
            List<ModelStructures.PostOrComment> replies = null, bool scoreHidden = false, int depth = 0, string id = null, string name = null,
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime),
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            return new Comment(Models, subreddit, title, author, body, bodyHtml, parentId, collapsedReason, collapsed, isSubmitter,
                replies, scoreHidden, depth, id, name, permalink, created, edited, score, upVotes, downVotes, removed, spam);
        }

        public Comment Comment()
        {
            return new Comment(Models);
        }

        public LinkPost LinkPost(ModelStructures.PostOrComment listing)
        {
            return new LinkPost(Models, listing);
        }

        public LinkPost LinkPost(string subreddit, string title, string author, string url, string thumbnail = null,
            int? thumbnailHeight = null, int? thumbnailWidth = null, JObject preview = null,
            string id = null, string name = null, string permalink = null, DateTime created = default(DateTime),
            DateTime edited = default(DateTime), int score = 0, int upVotes = 0, int downVotes = 0,
            bool removed = false, bool spam = false)
        {
            return new LinkPost(Models, subreddit, title, author, url, thumbnail, thumbnailHeight, thumbnailWidth, preview,
                id, name, permalink, created, edited, score, upVotes, downVotes, removed, spam);
        }

        public LinkPost LinkPost()
        {
            return new LinkPost(Models);
        }

        public SelfPost SelfPost(ModelStructures.PostOrComment listing)
        {
            return new SelfPost(Models, listing);
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
        public SelfPost SelfPost(string subreddit, string title, string author, string selfText, string selfTextHtml,
            string id = null, string name = null, string permalink = null, DateTime created = default(DateTime),
            DateTime edited = default(DateTime), int score = 0, int upVotes = 0, int downVotes = 0,
            bool removed = false, bool spam = false)
        {
            return new SelfPost(Models, subreddit, title, author, selfText, selfTextHtml, id, name, permalink, created,
                edited, score, upVotes, downVotes, removed, spam);
        }

        public SelfPost SelfPost()
        {
            return new SelfPost(Models);
        }

        public User User(ModelStructures.User user)
        {
            return new User(Models, user);
        }

        public User User(string id, string name, bool isFriend = false, bool profanityFilter = false, bool isSuspended = false,
            bool hasGoldSubscription = false, int numFriends = 0, bool IsVerified = false, bool hasNewModmail = false, bool over18 = false,
            bool isGold = false, bool isMod = false, bool hasVerifiedEmail = false, string iconImg = null, bool hasModmail = false, int linkKarma = 0, int inboxCount = 0,
            bool hasMail = false, DateTime created = default(DateTime), int commentKarma = 0, bool hasSubscribed = false)
        {
            return new User(Models, id, name, isFriend, profanityFilter, isSuspended, hasGoldSubscription, numFriends, IsVerified, hasNewModmail, over18, isGold, isMod,
                hasVerifiedEmail, iconImg, hasModmail, linkKarma, inboxCount, hasMail, created, commentKarma, hasSubscribed);
        }

        public User User()
        {
            return new User(Models);
        }

        public Subreddit Subreddit(ModelStructures.Subreddit subreddit)
        {
            return new Subreddit(Models, subreddit);
        }

        public Subreddit Subreddit(ModelStructures.SubredditChild subredditChild)
        {
            return new Subreddit(Models, subredditChild);
        }

        public Subreddit Subreddit(string name, string title, string description, string sidebar,
            string submissionText = null, string lang = "en", string subredditType = "public", string submissionType = "any",
            string submitLinkLabel = null, string submitTextLabel = null, bool wikiEnabled = false, bool over18 = false,
            bool allowDiscovery = true, bool allowSpoilers = true, bool showMedia = true, bool showMediaPreview = true,
            bool allowImages = true, bool allowVideos = true, bool collapseDeletedComments = false, string suggestedCommentSort = null,
            int commentScoreHideMins = 0, byte[] headerImage = null, byte[] iconImage = null, string primaryColor = null, string keyColor = null)
        {
            return new Subreddit(Models, name, title, description, sidebar, submissionText, lang, subredditType, submissionType, submitLinkLabel, submitTextLabel,
                wikiEnabled, over18, allowDiscovery, allowSpoilers, showMedia, showMediaPreview, allowImages, allowVideos, collapseDeletedComments,
                suggestedCommentSort, commentScoreHideMins, headerImage, iconImage, primaryColor, keyColor);
        }

        public Subreddit Subreddit(string name, string title = "", string description = "", string sidebar = "")
        {
            return new Subreddit(Models, name, title, description, sidebar);
        }

        public Subreddit Subreddit()
        {
            return new Subreddit(Models);
        }
    }
}
