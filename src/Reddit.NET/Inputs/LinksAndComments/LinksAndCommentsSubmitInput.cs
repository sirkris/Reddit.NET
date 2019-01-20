using System;

namespace Reddit.Inputs.LinksAndComments
{
    [Serializable]
    public class LinksAndCommentsSubmitInput : APITypeInput
    {
        /// <summary>
        /// boolean value
        /// </summary>
        public bool ad { get; set; }

        /// <summary>
        /// string value
        /// </summary>
        public string app { get; set; }

        /// <summary>
        /// extension used for redirects
        /// </summary>
        public string extension { get; set; }

        /// <summary>
        /// a string no longer than 36 characters
        /// </summary>
        public string flair_id { get; set; }

        /// <summary>
        /// a string no longer than 64 characters
        /// </summary>
        public string flair_text { get; set; }

        /// <summary>
        /// one of (link, self, image, video, videogif)
        /// </summary>
        public string kind { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool nsfw { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool resubmit { get; set; }

        /// <summary>
        /// JSON data
        /// </summary>
        public string richtext_json { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool sendreplies { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool spoiler { get; set; }

        /// <summary>
        /// name of a subreddit
        /// </summary>
        public string sr { get; set; }

        /// <summary>
        /// raw markdown text
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// title of the submission. Up to 300 characters long
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// a valid URL
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// a valid URL
        /// </summary>
        public string video_poster_url { get; set; }

        /// <summary>
        /// Submit a link to a subreddit.
        /// Submit will create a link or self-post in the subreddit sr with the title title.
        /// If kind is "link", then url is expected to be a valid URL to link to.
        /// Otherwise, text, if present, will be the body of the self-post unless richtext_json is present, in which case it will be converted into the body of the self-post.
        /// An error is thrown if both text and richtext_json are present.
        /// If a link with the same URL has already been submitted to the specified subreddit an error will be returned unless resubmit is true.
        /// extension is used for determining which view-type (e.g.json, compact etc.) to use for the redirect that is generated if the resubmit error occurs.
        /// </summary>
        /// <param name="ad">boolean value</param>
        /// <param name="app"></param>
        /// <param name="extension">extension used for redirects</param>
        /// <param name="flairId">a string no longer than 36 characters</param>
        /// <param name="flairText">a string no longer than 64 characters</param>
        /// <param name="kind">one of (link, self, image, video, videogif)</param>
        /// <param name="nsfw">boolean value</param>
        /// <param name="resubmit">boolean value</param>
        /// <param name="richtextJson">JSON data</param>
        /// <param name="sendReplies">boolean value</param>
        /// <param name="spoiler">boolean value</param>
        /// <param name="sr">name of a subreddit</param>
        /// <param name="text">raw markdown text</param>
        /// <param name="title">title of the submission. up to 300 characters long</param>
        /// <param name="url">a valid URL</param>
        /// <param name="videoPosterUrl">a valid URL</param>
        public LinksAndCommentsSubmitInput(bool ad = false, string app = "", string extension = "", string flairId = "", string flairText = "", 
            string kind = "", bool nsfw = false, bool resubmit = false, string richtextJson = "", bool sendReplies = true, bool spoiler = false, string sr = "", string text = "",
            string title = "", string url = "", string videoPosterUrl = "")
            : base()
        {
            this.ad = ad;
            this.app = app;
            this.extension = extension;
            flair_id = flairId;
            flair_text = flairText;
            this.kind = kind;
            this.nsfw = nsfw;
            this.resubmit = resubmit;
            richtext_json = richtextJson;
            sendreplies = sendReplies;
            this.spoiler = spoiler;
            this.sr = sr;
            this.text = text;
            this.title = title;
            this.url = url;
            video_poster_url = videoPosterUrl;
        }
    }
}
