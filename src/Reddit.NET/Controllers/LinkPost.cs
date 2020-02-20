using Newtonsoft.Json.Linq;
using Reddit.Exceptions;
using Reddit.Inputs.LinksAndComments;
using Reddit.Inputs.Listings;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.Controllers
{
    /// <summary>
    /// Controller class for link posts.
    /// </summary>
    public class LinkPost : Post
    {
        public JObject Preview
        {
            get
            {
                return Listing?.Preview;
            }
            set
            {
                if (Listing == null)
                {
                    Listing = new Things.Post(this);
                }

                Listing.Preview = value;
            }
        }

        /// <summary>
        /// The URL the LinkPost points to.
        /// </summary>
        public string URL
        {
            get
            {
                return Listing?.URL;
            }
            set
            {
                if (Listing == null)
                {
                    Listing = new Things.Post(this);
                }

                Listing.URL = value;
            }
        }

        public string Thumbnail
        {
            get
            {
                return Listing?.Thumbnail;
            }
            set
            {
                if (Listing == null)
                {
                    Listing = new Things.Post(this);
                }

                Listing.Thumbnail = value;
            }
        }

        public int? ThumbnailHeight
        {
            get
            {
                return Listing?.ThumbnailHeight;
            }
            set
            {
                if (Listing == null)
                {
                    Listing = new Things.Post(this);
                }

                Listing.ThumbnailHeight = value;
            }
        }

        public int? ThumbnailWidth
        {
            get
            {
                return Listing?.ThumbnailWidth;
            }
            set
            {
                if (Listing == null)
                {
                    Listing = new Things.Post(this);
                }

                Listing.ThumbnailWidth = value;
            }
        }

        /// <summary>
        /// Create a new link post controller instance from API return data.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="listing"></param>
        public LinkPost(Dispatch dispatch, Things.Post listing) : base(dispatch, listing) { }

        /// <summary>
        /// Create a new link post controller instance, populated manually.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="subreddit">The subreddit to which the post belongs</param>
        /// <param name="title">The title of the post</param>
        /// <param name="author">The post author's username</param>
        /// <param name="url">The link post URL</param>
        /// <param name="thumbnail"></param>
        /// <param name="thumbnailHeight"></param>
        /// <param name="thumbnailWidth"></param>
        /// <param name="preview"></param>
        /// <param name="id"></param>
        /// <param name="fullname"></param>
        /// <param name="permalink"></param>
        /// <param name="created"></param>
        /// <param name="edited"></param>
        /// <param name="score"></param>
        /// <param name="upVotes"></param>
        /// <param name="downVotes"></param>
        /// <param name="removed"></param>
        /// <param name="spam"></param>
        /// <param name="nsfw"></param>
        public LinkPost(Dispatch dispatch, string subreddit, string title, string author, string url, string thumbnail = null,
            int? thumbnailHeight = null, int? thumbnailWidth = null, JObject preview = null,
            string id = null, string fullname = null, string permalink = null, DateTime created = default(DateTime),
            DateTime edited = default(DateTime), int score = 0, int upVotes = 0, int downVotes = 0,
            bool removed = false, bool spam = false, bool nsfw = false)
            : base(dispatch, subreddit, title, author, id, fullname, permalink, created, edited, score, upVotes, downVotes,
                  removed, spam, nsfw)
        {
            Preview = preview;
            URL = url;
            Thumbnail = thumbnail;
            ThumbnailHeight = thumbnailHeight;
            ThumbnailWidth = thumbnailWidth;

            Listing = new Things.Post(this);
        }

        /// <summary>
        /// Create a new link post controller instance, populated only with its fullname.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="fullname">Fullname of the post</param>
        public LinkPost(Dispatch dispatch, string fullname) : base(dispatch, fullname) { }

        /// <summary>
        /// Create a new link post controller instance, populated only with its fullname and subreddit.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="fullname">Fullname of the post</param>
        /// <param name="subreddit">A valid subreddit instance</param>
        public LinkPost(Dispatch dispatch, string fullname, string subreddit) : base(dispatch, fullname, subreddit) { }

        /// <summary>
        /// Create a new link post controller instance, populated manually.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="subreddit">A valid subreddit instance</param>
        /// <param name="title">The title of the post</param>
        /// <param name="url">The link post URL</param>
        /// <param name="author">The post author's username</param>
        /// <param name="thumbnail"></param>
        /// <param name="thumbnailHeight"></param>
        /// <param name="thumbnailWidth"></param>
        /// <param name="preview"></param>
        /// <param name="id"></param>
        /// <param name="fullname"></param>
        /// <param name="permalink"></param>
        /// <param name="created"></param>
        /// <param name="edited"></param>
        /// <param name="score"></param>
        /// <param name="upVotes"></param>
        /// <param name="downVotes"></param>
        /// <param name="removed"></param>
        /// <param name="spam"></param>
        public LinkPost(Dispatch dispatch, string subreddit, string title = null, string url = null, string author = null, 
            string thumbnail = null, int? thumbnailHeight = null, int? thumbnailWidth = null, JObject preview = null,
            string id = null, string fullname = null, string permalink = null, DateTime created = default(DateTime),
            DateTime edited = default(DateTime), int score = 0, int upVotes = 0, int downVotes = 0,
            bool removed = false, bool spam = false)
            : base(dispatch, subreddit, title, author, id, fullname, permalink, created,
                edited, score, upVotes, downVotes, removed, spam)
        {
            Preview = preview;
            URL = url;
            Thumbnail = thumbnail;
            ThumbnailHeight = thumbnailHeight;
            ThumbnailWidth = thumbnailWidth;

            Listing = new Things.Post(this);
        }

        /// <summary>
        /// Create a new link post controller instance, populated from post result data.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="postResultShortData"></param>
        /// <param name="linkPost"></param>
        public LinkPost(Dispatch dispatch, PostResultShortData postResultShortData, LinkPost linkPost)
            : base(dispatch, linkPost.Subreddit, linkPost.Title, linkPost.Author, postResultShortData.Id, postResultShortData.Name,
                  linkPost.Permalink, linkPost.Created, linkPost.Edited, linkPost.Score, linkPost.UpVotes, linkPost.DownVotes,
                  linkPost.Removed, linkPost.Spam, linkPost.NSFW)
        {
            Preview = linkPost.Preview;
            URL = linkPost.URL;
            Thumbnail = linkPost.Thumbnail;
            ThumbnailHeight = linkPost.ThumbnailHeight;
            ThumbnailWidth = linkPost.ThumbnailWidth;

            Listing = new Things.Post(this);
        }

        /// <summary>
        /// Create a new link post controller instance, populated from SelfPost data.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="selfPost"></param>
        public LinkPost(Dispatch dispatch, SelfPost selfPost)
            : base(dispatch, selfPost.Subreddit, selfPost.Title, selfPost.Author, nsfw: selfPost.NSFW)
        {
            Listing = new Things.Post(this);
        }

        /// <summary>
        /// Create an empty link post controller instance.
        /// </summary>
        /// <param name="dispatch"></param>
        public LinkPost(Dispatch dispatch) : base(dispatch) { }

        /// <summary>
        /// Submit this link post to Reddit.
        /// </summary>
        /// <param name="resubmit">boolean value</param>
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
        public LinkPost Submit(bool resubmit = false, bool ad = false, string app = "", string extension = "",
            string flairId = "", string flairText = "", string gRecapthaResponse = "", bool sendReplies = true, bool spoiler = false,
            string videoPosterUrl = "")
        {
            return Submit(new LinksAndCommentsSubmitInput(ad, app, extension, flairId, flairText,
                "link", NSFW, resubmit, null, sendReplies, spoiler, Subreddit, null, Title, URL, videoPosterUrl), gRecapthaResponse);
        }

        /// <summary>
        /// Submit this link post to Reddit asynchronously.  This instance will automatically be updated with the resulting fullname/id.
        /// </summary>
        /// <param name="resubmit">boolean value</param>
        /// <param name="ad">boolean value</param>
        /// <param name="app"></param>
        /// <param name="extension">extension used for redirects</param>
        /// <param name="flairId">a string no longer than 36 characters</param>
        /// <param name="flairText">a string no longer than 64 characters</param>
        /// <param name="gRecapthaResponse"></param>
        /// <param name="sendReplies">boolean value</param>
        /// <param name="spoiler">boolean value</param>
        /// <param name="videoPosterUrl">a valid URL</param>
        public async Task<LinkPost> SubmitAsync(bool resubmit = false, bool ad = false, string app = "", string extension = "",
            string flairId = "", string flairText = "", string gRecapthaResponse = "", bool sendReplies = true, bool spoiler = false,
            string videoPosterUrl = "")
        {
            return await SubmitAsync(new LinksAndCommentsSubmitInput(ad, app, extension, flairId, flairText,
                "link", NSFW, resubmit, null, sendReplies, spoiler, Subreddit, null, Title, URL, videoPosterUrl), gRecapthaResponse);
        }

        /// <summary>
        /// Submit this link post to Reddit.
        /// </summary>
        /// <param name="linksAndCommentsSubmitInput">A valid LinksAndCommentsSubmitInput instance</param>
        /// <param name="gRecapthaResponse"></param>
        /// <returns>A copy of this instance populated with the ID and Fullname returned by the API.</returns>
        public LinkPost Submit(LinksAndCommentsSubmitInput linksAndCommentsSubmitInput, string gRecapthaResponse = "")
        {
            return new LinkPost(Dispatch, Validate(Dispatch.LinksAndComments.Submit(linksAndCommentsSubmitInput, gRecapthaResponse)).JSON.Data, this);
        }

        /// <summary>
        /// Submit this link post to Reddit asynchronously.
        /// </summary>
        /// <param name="linksAndCommentsSubmitInput">A valid LinksAndCommentsSubmitInput instance</param>
        /// <param name="gRecapthaResponse"></param>
        public async Task<LinkPost> SubmitAsync(LinksAndCommentsSubmitInput linksAndCommentsSubmitInput, string gRecapthaResponse = "")
        {
            return new LinkPost(Dispatch, Validate(await Dispatch.LinksAndComments.SubmitAsync(linksAndCommentsSubmitInput, gRecapthaResponse)).JSON.Data, this);
        }

        /// <summary>
        /// Cross-post this to another subreddit.
        /// </summary>
        /// <param name="subreddit">The name of the subreddit being xposted to</param>
        /// <returns>The resulting post data.</returns>
        public LinkPost XPostTo(string subreddit)
        {
            LinkPost res = this;
            res.Subreddit = subreddit;

            return Validate(res.Submit());
        }

        /// <summary>
        /// Cross-post this to another subreddit asynchronously.
        /// </summary>
        /// <param name="subreddit">The name of the subreddit being xposted to</param>
        /// <returns>The resulting post data.</returns>
        public async Task<LinkPost> XPostToAsync(string subreddit)
        {
            LinkPost res = this;
            res.Subreddit = subreddit;

            return Validate(await res.SubmitAsync());
        }

        /// <summary>
        /// Return information about the current LinkPost instance.
        /// </summary>
        /// <returns>An instance of this class populated with the retrieved data.</returns>
        public new LinkPost About()
        {
            Info info = Validate(Dispatch.LinksAndComments.Info(Fullname, Subreddit));
            if (info == null
                || info.Posts == null
                || info.Posts.Count == 0
                || !Fullname.Equals(info.Posts[0].Name))
            {
                throw new RedditControllerException("Unable to retrieve post data.");
            }

            return new LinkPost(Dispatch, info.Posts[0]);
        }

        /// <summary>
        /// Return a list of other submissions of the same URL.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="crosspostsOnly">boolean value</param>
        /// <param name="sort">one of (num_comments, new)</param>
        /// <param name="sr">subreddit name</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of matching posts.</returns>
        public List<LinkPost> GetDuplicates(string after = "", string before = "", bool crosspostsOnly = false, string sort = "new", string sr = "",
            int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            return GetDuplicates(new ListingsGetDuplicatesInput(sr, after, before, crosspostsOnly, sort, count, limit, show, srDetail));
        }

        /// <summary>
        /// Return a list of other submissions of the same URL.
        /// </summary>
        /// <param name="listingsGetDuplicatesInput">A valid ListingsGetDuplicatesInput instance</param>
        /// <returns>A list of matching posts.</returns>
        public List<LinkPost> GetDuplicates(ListingsGetDuplicatesInput listingsGetDuplicatesInput)
        {
            Lists.GetPosts(Validate(Dispatch.Listings.GetDuplicates(Id, listingsGetDuplicatesInput)), Dispatch,
                out List<LinkPost> linkPosts);

            return linkPosts;
        }

        /// <summary>
        /// Return a list of crossposts.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="sort">one of (num_comments, new)</param>
        /// <param name="sr">subreddit name</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of matching posts.</returns>
        public List<LinkPost> GetCrossPosts(string after = "", string before = "", string sort = "new", string sr = "",
            int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            return GetDuplicates(after, before, true, sort, sr, count, limit, show, srDetail);
        }

        /// <summary>
        /// Return a list of crossposts.
        /// </summary>
        /// <param name="listingsGetDuplicatesInput">A valid ListingsGetDuplicatesInput instance</param>
        /// <returns>A list of matching posts.</returns>
        public List<LinkPost> GetCrossPosts(ListingsGetDuplicatesInput listingsGetDuplicatesInput)
        {
            listingsGetDuplicatesInput.crossposts_only = true;

            return GetDuplicates(listingsGetDuplicatesInput);
        }
    }
}
