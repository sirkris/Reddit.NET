using Newtonsoft.Json.Linq;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Threading.Tasks;

namespace Reddit.NET.Controllers
{
    public class LinkPost : Post
    {
        public JObject Preview;
        public string URL;
        public string Thumbnail;
        public int? ThumbnailHeight;
        public int? ThumbnailWidth;

        public LinkPost(Dispatch dispatch, Models.Structures.Post listing) : base(dispatch, listing)
        {
            Preview = listing.Preview;
            URL = listing.URL;
            Thumbnail = listing.Thumbnail;
            ThumbnailHeight = listing.ThumbnailHeight;
            ThumbnailWidth = listing.ThumbnailWidth;
        }

        public LinkPost(Dispatch dispatch, string subreddit, string title, string author, string url, string thumbnail = null,
            int? thumbnailHeight = null, int? thumbnailWidth = null, JObject preview = null,
            string id = null, string name = null, string permalink = null, DateTime created = default(DateTime),
            DateTime edited = default(DateTime), int score = 0, int upVotes = 0, int downVotes = 0,
            bool removed = false, bool spam = false, bool nsfw = false)
            : base(dispatch, subreddit, title, author, id, name, permalink, created, edited, score, upVotes, downVotes,
                  removed, spam, nsfw)
        {
            Preview = preview;
            URL = url;
            Thumbnail = thumbnail;
            ThumbnailHeight = thumbnailHeight;
            ThumbnailWidth = thumbnailWidth;

            Listing = new RedditThings.Post(this);
        }

        public LinkPost(Dispatch dispatch) : base(dispatch) { }

        /// <summary>
        /// Submit this link post to Reddit.  This instance will automatically be updated with the resulting fullname/id.
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
        /// <returns>An object containing the id, name, and URL of the newly created post.</returns>
        public override RedditThings.PostResultShortData Submit(bool resubmit = false, bool ad = false, string app = "", string extension = "",
            string flairId = "", string flairText = "", string gRecapthaResponse = "", bool sendReplies = true, bool spoiler = false,
            string videoPosterUrl = "")
        {
            RedditThings.PostResultShortData res = Validate(Dispatch.LinksAndComments.Submit(ad, app, extension, flairId, flairText,
                gRecapthaResponse, "link", NSFW, resubmit, null, sendReplies, spoiler, Subreddit, null, Title, URL, videoPosterUrl)).JSON.Data;

            Id = res.Id;
            Fullname = "t3_" + Id;

            return res;
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
        public async void SubmitAsync(bool resubmit = false, bool ad = false, string app = "", string extension = "",
            string flairId = "", string flairText = "", string gRecapthaResponse = "", bool sendReplies = true, bool spoiler = false,
            string videoPosterUrl = "")
        {
            await Task.Run(() =>
            {
                Submit(resubmit, ad, app, extension, flairId, flairText, gRecapthaResponse, sendReplies, spoiler, videoPosterUrl);
            });
        }
    }
}
