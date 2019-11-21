using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.Controllers;
using Reddit.Models.Converters;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class Post
    {
        [JsonProperty("approved_at_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime ApprovedAtUTC { get; set; }

        [JsonProperty("subreddit")]
        public string Subreddit { get; set; }

        [JsonProperty("selftext")]
        public string SelfText { get; set; }

        [JsonProperty("user_reports")]
        public JArray UserReports { get; set; }

        [JsonProperty("saved")]
        public bool Saved { get; set; }

        [JsonProperty("mod_reason_title")]
        public string ModReasonTitle { get; set; }

        [JsonProperty("gilded")]
        public int Gilded { get; set; }

        [JsonProperty("clicked")]
        public bool Clicked { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        // TODO - Only had an empty example so not sure if the structure is right.  --Kris
        [JsonProperty("link_flair_richtext")]
        public object PublicFlairRichtext { get; set; }

        [JsonProperty("subreddit_name_prefixed")]
        public string SubredditNamePrefixed { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        // TODO - No idea what this is.  --Kris
        [JsonProperty("pwls")]
        public string Pwls { get; set; }

        [JsonProperty("link_flair_css_class")]
        public string LinkFlairCSSClass { get; set; }

        [JsonProperty("downs")]
        public int Downs { get; set; }

        [JsonProperty("thumbnail_height")]
        public int? ThumbnailHeight { get; set; }

        [JsonProperty("parent_whitelist_status")]
        public string ParentWhitelistStatus { get; set; }

        [JsonProperty("hide_score")]
        public bool HideScore { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("quarantine")]
        public bool Quarantine { get; set; }

        [JsonProperty("link_flair_text_color")]
        public string LinkFlairTextColor { get; set; }

        [JsonProperty("upvote_ratio")]
        public double UpvoteRatio { get; set; }

        [JsonProperty("author_flair_background_color")]
        public string AuthorFlairBackgroundColor { get; set; }

        [JsonProperty("subreddit_type")]
        public string SubredditType { get; set; }

        [JsonProperty("ups")]
        public int Ups { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        // TODO - No idea what this does.  It's always empty when I see it.  --Kris
        [JsonProperty("media_embed")]
        public object MediaEmbed { get; set; }

        [JsonProperty("thumbnail_width")]
        public int? ThumbnailWidth { get; set; }

        [JsonProperty("author_flair_template_id")]
        public string AuthorFlairTemplateId { get; set; }

        [JsonProperty("is_original_content")]
        public bool IsOriginalContent { get; set; }

        [JsonProperty("author_fullname")]
        public string AuthorFullname { get; set; }

        [JsonProperty("secure_media")]
        public object SecureMedia { get; set; }

        [JsonProperty("is_reddit_media_domain")]
        public bool IsRedditMediaDomain { get; set; }

        [JsonProperty("is_meta")]
        public bool IsMeta { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("num_comments")]
        public int NumComments { get; set; }

        // TODO - Same as MediaEmbed.  Not sure what this is supposed to be.  --Kris
        [JsonProperty("secure_media_embed")]
        public object SecureMediaEmbed { get; set; }

        [JsonProperty("link_flair_text")]
        public string LinkFlairText { get; set; }

        [JsonProperty("can_mod_post")]
        public bool CanModPost { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("approved_by")]
        public string ApprovedBy { get; set; }

        [JsonProperty("ignore_reports")]
        public bool IgnoreReports { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("edited")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime Edited { get; set; }

        [JsonProperty("author_flair_css_class")]
        public string AuthorFlairCSSClass { get; set; }

        [JsonProperty("previous_visits")]
        //[JsonConverter(typeof(List<TimestampConvert>))]
        public object PreviousVisits { get; set; }

        // TODO - Is this a list or a string or what?  --Kris
        [JsonProperty("author_flair_richtext")]
        public List<object> AuthorFlairRichtext { get; set; }

        [JsonProperty("gildings")]
        public Dictionary<string, int> Gildings { get; set; }

        [JsonProperty("post_hint")]
        public string PostHint { get; set; }

        [JsonProperty("content_categories")]
        public object ContentCategories { get; set; }

        [JsonProperty("is_self")]
        public bool IsSelf { get; set; }

        [JsonProperty("mod_note")]
        public string ModNote { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(LocalTimestampConverter))]
        [Obsolete("Using this date can lead to unexpected results.  It is recommended that you use " + nameof(CreatedUTC) + " instead.")]
        public DateTime Created { get; set; }

        [JsonProperty("link_flair_type")]
        public string LinkFlairType { get; set; }

        // TODO - No idea what this is.  --Kris
        [JsonProperty("wls")]
        public string Wls { get; set; }

        [JsonProperty("banned_by")]
        public string BannedBy { get; set; }

        [JsonProperty("author_flair_type")]
        public string AuthorFlairType { get; set; }

        [JsonProperty("contest_mode")]
        public bool ContestMode { get; set; }

        [JsonProperty("selftext_html")]
        public string SelfTextHTML { get; set; }

        [JsonProperty("likes")]
        public bool? Likes { get; set; }

        [JsonProperty("suggested_sort")]
        public string SuggestedSort { get; set; }

        [JsonProperty("banned_at_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime BannedAtUTC { get; set; }

        [JsonProperty("view_count")]
        public int? ViewCount { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("no_follow")]
        public bool NoFollow { get; set; }

        [JsonProperty("spam")]
        public bool Spam { get; set; }

        [JsonProperty("is_crosspostable")]
        public bool IsCrosspostable { get; set; }

        [JsonProperty("pinned")]
        public bool Pinned { get; set; }

        [JsonProperty("over_18")]
        public bool Over18 { get; set; }

        [JsonProperty("preview")]
        public JObject Preview { get; set; }

        [JsonProperty("media_only")]
        public bool MediaOnly { get; set; }

        [JsonProperty("link_flair_template_id")]
        public string LinkFlairTemplateId { get; set; }

        [JsonProperty("can_gild")]
        public bool CanGild { get; set; }

        [JsonProperty("removed")]
        public bool Removed { get; set; }

        [JsonProperty("spoiler")]
        public bool Spoiler { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("author_flair_text")]
        public string AuthorFlairText { get; set; }

        [JsonProperty("rte_mode")]
        public string RteMode { get; set; }

        [JsonProperty("visited")]
        public bool Visited { get; set; }

        [JsonProperty("num_reports")]
        public int? NumReports { get; set; }

        [JsonProperty("distinguished")]
        public string Distinguished { get; set; }

        [JsonProperty("subreddit_id")]
        public string SubredditId { get; set; }

        [JsonProperty("mod_reason_by")]
        public string ModReasonBy { get; set; }

        [JsonProperty("removal_reason")]
        public string RemovalReason { get; set; }

        [JsonProperty("link_flair_background_color")]
        public string LinkFlairBackgroundColor { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("report_reasons")]
        public List<string> ReportReasons { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("num_crossposts")]
        public int NumCrossposts { get; set; }

        [JsonProperty("media")]
        public object Media { get; set; }

        [JsonProperty("send_replies")]
        public bool SendReplies { get; set; }

        [JsonProperty("approved")]
        public bool Approved { get; set; }

        [JsonProperty("author_flair_text_color")]
        public string AuthorFlairTextColor { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("whitelist_status")]
        public string WhitelistStatus { get; set; }

        [JsonProperty("stickied")]
        public bool Stickied { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("subreddit_subscribers")]
        public int SubredditSubscribers { get; set; }

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC { get; set; }

        [JsonProperty("mod_reports")]
        public JArray ModReports { get; set; }

        [JsonProperty("is_video")]
        public bool IsVideo { get; set; }

        [JsonProperty("sr_detail")]
        public Subreddit SrDetail { get; set; }

        public Post(Controllers.Post post)
        {
            ImportFromPost(post);
        }

        public Post(SelfPost selfPost)
        {
            ImportFromSelfPost(selfPost);
        }

        public Post(LinkPost linkPost)
        {
            ImportFromLinkPost(linkPost);
        }

        private void ImportFromPost(Controllers.Post post)
        {
            Subreddit = post.Subreddit;
            Title = post.Title;
            Author = post.Author;
            Id = post.Id;
            Name = post.Fullname;
            Permalink = post.Permalink;
            CreatedUTC = post.Created;
            Edited = post.Edited;
            Score = post.Score;
            Ups = post.UpVotes;
            Downs = post.DownVotes;
            Removed = post.Removed;
            Spam = post.Spam;
            Over18 = post.NSFW;
        }

        private void ImportFromSelfPost(SelfPost selfPost)
        {
            ImportFromPost(selfPost);

            SelfText = selfPost.SelfText;
            SelfTextHTML = selfPost.SelfTextHTML;
        }

        private void ImportFromLinkPost(LinkPost linkPost)
        {
            ImportFromPost(linkPost);

            Preview = linkPost.Preview;
            URL = linkPost.URL;
            Thumbnail = linkPost.Thumbnail;
            ThumbnailHeight = linkPost.ThumbnailHeight;
            ThumbnailWidth = linkPost.ThumbnailWidth;
        }

        public Post() { }
    }
}
