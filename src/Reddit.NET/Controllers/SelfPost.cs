using Reddit.Controllers.Internal;
using Reddit.Exceptions;
using Reddit.Inputs.LinksAndComments;
using Reddit.Things;
using System;
using System.Threading.Tasks;

namespace Reddit.Controllers
{
    /// <summary>
    /// Controller class for self posts.
    /// </summary>
    public class SelfPost : Post
    {
        public string SelfText
        {
            get
            {
                return selfText;
            }
            set
            {
                selfText = Parsing.HtmlDecode(value);
            }
        }
        private string selfText;

        public string SelfTextHTML
        {
            get
            {
                return selfTextHtml;
            }
            set
            {
                selfTextHtml = Parsing.HtmlDecode(value);
            }
        }
        private string selfTextHtml;

        /// <summary>
        /// Create new SelfPost instance from Reddit API listing.
        /// </summary>
        /// <param name="dispatch">An instance of the Dispatch controller</param>
        /// <param name="listing">Listing returned by Reddit API.</param>
        public SelfPost(Dispatch dispatch, Things.Post listing) : base(dispatch, listing)
        {
            SelfText = listing.SelfText;
            SelfTextHTML = listing.SelfTextHTML;
        }

        /// <summary>
        /// Create a new SelfPost instance and populate manually.
        /// </summary>
        /// <param name="dispatch">An instance of the Dispatch controller</param>
        /// <param name="subreddit">The subreddit the post belongs to.</param>
        /// <param name="title">Post title.</param>
        /// <param name="author">Reddit user who authored the post.</param>
        /// <param name="selfText">The post body.</param>
        /// <param name="selfTextHtml">The HTML-formateed post body.</param>
        /// <param name="id">Post ID.</param>
        /// <param name="fullname">Post fullname.</param>
        /// <param name="permalink">Permalink of post.</param>
        /// <param name="created">When the post was created.</param>
        /// <param name="edited">When the post was last edited.</param>
        /// <param name="score">Net vote score.</param>
        /// <param name="upVotes">Number of upvotes.</param>
        /// <param name="downVotes">Number of downvotes.</param>
        /// <param name="removed">Whether the post was removed.</param>
        /// <param name="spam">Whether the post was marked as spam.</param>
        public SelfPost(Dispatch dispatch, string subreddit, string title, string author, string selfText, string selfTextHtml,
            string id = null, string fullname = null, string permalink = null, DateTime created = default(DateTime),
            DateTime edited = default(DateTime), int score = 0, int upVotes = 0, int downVotes = 0,
            bool removed = false, bool spam = false)
            : base(dispatch, subreddit, title, author, id, fullname, permalink, created, edited, score, upVotes, downVotes,
                  removed, spam)
        {
            SelfText = selfText;
            SelfTextHTML = selfTextHtml;

            Listing = new Things.Post(this);
        }

        /// <summary>
        /// Create a new SelfPost instance populated only with its Fullname.
        /// Useful for About() queries (e.g. new SelfPost("t3_whatever").About() will retrieve a new SelfPost by its fullname).
        /// </summary>
        /// <param name="dispatch">An instance of the Dispatch controller</param>
        /// <param name="fullname">fullname of a thing</param>
        public SelfPost(Dispatch dispatch, string fullname) : base(dispatch, fullname) { }

        /// <summary>
        /// Create a new SelfPost instance populated with its Fullname and Subreddit.
        /// </summary>
        /// <param name="dispatch">An instance of the Dispatch controller</param>
        /// <param name="fullname">fullname of a thing</param>
        /// <param name="subreddit">The subreddit where the post exists</param>
        public SelfPost(Dispatch dispatch, string fullname, string subreddit) : base(dispatch, fullname, subreddit) { }

        /// <summary>
        /// Create a new SelfPost instance populated with its Subreddit, an ID/Fullname returned by the API, and other specified values.
        /// </summary>
        /// <param name="dispatch">An instance of the Dispatch controller</param>
        /// <param name="postResultShortData">Data returned by the Reddit API when creating a new post</param>
        /// <param name="selfPost">The SelfPost instance that executed the submission</param>
        public SelfPost(Dispatch dispatch, PostResultShortData postResultShortData, SelfPost selfPost)
            : base(dispatch, selfPost.Subreddit, selfPost.Title, selfPost.Author, postResultShortData.Id, postResultShortData.Name,
                  selfPost.Permalink, selfPost.Created, selfPost.Edited, selfPost.Score, selfPost.UpVotes, selfPost.DownVotes,
                  selfPost.Removed, selfPost.Spam, selfPost.NSFW)
        {
            SelfText = selfPost.SelfText;
            SelfTextHTML = selfPost.SelfTextHTML;

            Listing = new Things.Post(this);
        }

        /// <summary>
        /// Create a new self post controller instance, populated from LinkPost data.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="linkPost"></param>
        public SelfPost(Dispatch dispatch, LinkPost linkPost)
            : base(dispatch, linkPost.Subreddit, linkPost.Title, linkPost.Author, nsfw: linkPost.NSFW)
        {
            Listing = new Things.Post(this);
        }

        /// <summary>
        /// Create an empty SelfPost instance.
        /// </summary>
        /// <param name="dispatch">An instance of the Dispatch controller</param>
        public SelfPost(Dispatch dispatch) : base(dispatch) { }

        /// <summary>
        /// Set the self text manually without any automatic decoding.
        /// </summary>
        /// <param name="value">The self text value you wish to set</param>
        public void SetSelfText(string value)
        {
            selfText = value;
        }

        /// <summary>
        /// Set the self text HTML manually without any automatic decoding.
        /// </summary>
        /// <param name="value">The self text HTML value you wish to set</param>
        public void SetSelfTextHTML(string value)
        {
            selfTextHtml = value;
        }

        /// <summary>
        /// Submit this self post to Reddit.
        /// </summary>
        /// <param name="ad">boolean value</param>
        /// <param name="app"></param>
        /// <param name="extension">extension used for redirects</param>
        /// <param name="flairId">a string no longer than 36 characters</param>
        /// <param name="flairText">a string no longer than 64 characters</param>
        /// <param name="gRecapthaResponse"></param>
        /// <param name="sendReplies">boolean value</param>
        /// <param name="spoiler">boolean value</param>
        /// <param name="videoPosterUrl">a valid URL</param>
        /// <returns>A copy of this instance populated with the ID and Fullname returned by the API.</returns>
        public SelfPost Submit(bool ad = false, string app = "", string extension = "",
            string flairId = "", string flairText = "", string gRecapthaResponse = "", bool sendReplies = true, bool spoiler = false,
            string videoPosterUrl = "")
        {
            return new SelfPost(Dispatch, Validate(Dispatch.LinksAndComments.Submit(new LinksAndCommentsSubmitInput(ad, app, extension, flairId, flairText,
                "self", NSFW, false, null, sendReplies, spoiler, Subreddit, SelfText, Title, null, videoPosterUrl), gRecapthaResponse)).JSON.Data, this);
        }

        /// <summary>
        /// Submit this self post to Reddit asynchronously.  This instance will automatically be updated with the resulting fullname/id.
        /// </summary>
        /// <param name="ad">boolean value</param>
        /// <param name="app"></param>
        /// <param name="extension">extension used for redirects</param>
        /// <param name="flairId">a string no longer than 36 characters</param>
        /// <param name="flairText">a string no longer than 64 characters</param>
        /// <param name="gRecapthaResponse"></param>
        /// <param name="sendReplies">boolean value</param>
        /// <param name="spoiler">boolean value</param>
        /// <param name="videoPosterUrl">a valid URL</param>
        public async Task<SelfPost> SubmitAsync(bool ad = false, string app = "", string extension = "",
            string flairId = "", string flairText = "", string gRecapthaResponse = "", bool sendReplies = true, bool spoiler = false,
            string videoPosterUrl = "")
        {
            return new SelfPost(Dispatch, Validate(await Dispatch.LinksAndComments.SubmitAsync(new LinksAndCommentsSubmitInput(ad, app, extension, flairId, flairText,
                "self", NSFW, false, null, sendReplies, spoiler, Subreddit, SelfText, Title, null, videoPosterUrl), gRecapthaResponse)).JSON.Data, this);
        }

        /// <summary>
        /// Submit this self post to Reddit.
        /// </summary>
        /// <param name="linksAndCommentsSubmitInput">A valid LinksAndCommentsSubmitInput instance</param>
        /// <param name="gRecapthaResponse"></param>
        /// <returns>A copy of this instance populated with the ID and Fullname returned by the API.</returns>
        public SelfPost Submit(LinksAndCommentsSubmitInput linksAndCommentsSubmitInput, string gRecapthaResponse = "")
        {
            return new SelfPost(Dispatch, Validate(Dispatch.LinksAndComments.Submit(linksAndCommentsSubmitInput, gRecapthaResponse)).JSON.Data, this);
        }

        /// <summary>
        /// Submit this self post to Reddit asynchronously.  This instance will automatically be updated with the resulting fullname/id.
        /// </summary>
        /// <param name="linksAndCommentsSubmitInput">A valid LinksAndCommentsSubmitInput instance</param>
        /// <param name="gRecapthaResponse"></param>
        public async Task<SelfPost> SubmitAsync(LinksAndCommentsSubmitInput linksAndCommentsSubmitInput, string gRecapthaResponse = "")
        {
            return new SelfPost(Dispatch, Validate(await Dispatch.LinksAndComments.SubmitAsync(linksAndCommentsSubmitInput, gRecapthaResponse)).JSON.Data, this);
        }

        /// <summary>
        /// Cross-post this to another subreddit.
        /// </summary>
        /// <param name="subreddit">The name of the subreddit being xposted to</param>
        /// <param name="creditOriginSub">Whether to include an attribution to the source subreddit in the title (default: true)</param>
        /// <returns>The resulting post data.</returns>
        public SelfPost XPostTo(string subreddit, bool creditOriginSub = true)
        {
            SelfPost res = this;
            res.Subreddit = subreddit;
            if (creditOriginSub)
            {
                res.Title += " • r/" + Subreddit;
            }

            return Validate(res.Submit());
        }

        /// <summary>
        /// Cross-post this to another subreddit asynchronously.
        /// </summary>
        /// <param name="subreddit">The name of the subreddit being xposted to</param>
        /// <param name="creditOriginSub">Whether to include an attribution to the source subreddit in the title (default: true)</param>
        /// <returns>The resulting post data.</returns>
        public async Task<SelfPost> XPostToAsync(string subreddit, bool creditOriginSub = true)
        {
            SelfPost res = this;
            res.Subreddit = subreddit;
            if (creditOriginSub)
            {
                res.Title += " • r/" + Subreddit;
            }

            return Validate(await res.SubmitAsync());
        }

        /// <summary>
        /// Link to this post from another subreddit.
        /// </summary>
        /// <param name="subreddit">The name of the subreddit being xposted to</param>
        /// <param name="creditOriginSub">Whether to include an attribution to the source subreddit in the title (default: true)</param>
        /// <param name="np">Whether to use np.reddit.com as the URL host (default: true)</param>
        /// <returns>The resulting post data.</returns>
        public LinkPost XPostToAsLink(string subreddit, bool creditOriginSub = true, bool np = true)
        {
            return Validate(PrepareXLinkPost(subreddit, creditOriginSub, np).Submit());
        }

        /// <summary>
        /// Link to this post from another subreddit asynchronously.
        /// </summary>
        /// <param name="subreddit">The name of the subreddit being xposted to</param>
        /// <param name="creditOriginSub">Whether to include an attribution to the source subreddit in the title (default: true)</param>
        /// <param name="np">Whether to use np.reddit.com as the URL host (default: true)</param>
        /// <returns>The resulting post data.</returns>
        public async Task<LinkPost> XPostToAsLinkAsync(string subreddit, bool creditOriginSub = true, bool np = true)
        {
            return Validate(await PrepareXLinkPost(subreddit, creditOriginSub, np).SubmitAsync());
        }

        /// <summary>
        /// Prepare a LinkPost object for cross-posting.
        /// </summary>
        /// <param name="subreddit">The name of the subreddit being xposted to</param>
        /// <param name="creditOriginSub">Whether to include an attribution to the source subreddit in the title (default: true)</param>
        /// <param name="np">Whether to use np.reddit.com as the URL host (default: true)</param>
        /// <returns>The resulting post data.</returns>
        private LinkPost PrepareXLinkPost(string subreddit, bool creditOriginSub = true, bool np = true)
        {
            LinkPost res = new LinkPost(Dispatch, this)
            {
                Subreddit = subreddit
            };

            if (creditOriginSub)
            {
                res.Title += " • r/" + Subreddit;
            }

            res.URL = (np ? res.Permalink.Replace("www.reddit.com", "np.reddit.com") : res.Permalink);

            return res;
        }

        /// <summary>
        /// Edit the body text of this self post.  This instance will be automatically updated with the return data.
        /// </summary>
        /// <param name="text">raw markdown text</param>
        /// <returns>This instance populated with the modified post data returned by the API.</returns>
        public SelfPost Edit(string text)
        {
            Import(Validate(Dispatch.LinksAndComments.EditUserText(new LinksAndCommentsThingInput(text, Fullname))).JSON.Data.Things[0].Data);

            return this;
        }

        /// <summary>
        /// Edit the body text of this self post asynchronously.  This instance will be automatically updated with the return data.
        /// </summary>
        /// <param name="text">raw markdown text</param>
        public async Task<SelfPost> EditAsync(string text)
        {
            Import(Validate(await Dispatch.LinksAndComments.EditUserTextAsync(new LinksAndCommentsThingInput(text, Fullname))).JSON.Data.Things[0].Data);

            return this;
        }

        /// <summary>
        /// Return information about the current SelfPost instance.
        /// </summary>
        /// <returns>An instance of this class populated with the retrieved data.</returns>
        public new SelfPost About()
        {
            Info info = Validate(Dispatch.LinksAndComments.Info(Fullname, Subreddit));
            if (info == null
                || info.Posts == null
                || info.Posts.Count == 0
                || !Fullname.Equals(info.Posts[0].Name))
            {
                throw new RedditControllerException("Unable to retrieve post data.");
            }

            return new SelfPost(Dispatch, info.Posts[0]);
        }
    }
}
