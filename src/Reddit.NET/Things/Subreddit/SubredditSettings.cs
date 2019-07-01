using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class SubredditSettings
    {
        [JsonProperty("default_set")]
        public bool DefaultSet { get; set; }

        [JsonProperty("subreddit_id")]
        public string SubredditId { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("allow_images")]
        public bool AllowImages { get; set; }

        [JsonProperty("free_form_reports")]
        public bool FreeFormReports { get; set; }

        [JsonProperty("show_media")]
        public bool ShowMedia { get; set; }

        [JsonProperty("wiki_edit_age")]
        public int WikiEditAge { get; set; }

        [JsonProperty("submit_text")]
        public string SubmitText { get; set; }

        [JsonProperty("spam_links")]
        public string SpamLinks { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("collapse_deleted_comments")]
        public bool CollapseDeletedComments { get; set; }

        [JsonProperty("wikimode")]
        public string WikiMode { get; set; }

        [JsonProperty("over_18")]
        public bool Over18 { get; set; }

        [JsonProperty("allow_videos")]
        public bool AllowVideos { get; set; }

        [JsonProperty("spoilers_enabled")]
        public bool SpoilersEnabled { get; set; }

        [JsonProperty("suggested_comment_sort")]
        public string SuggestedCommentSort { get; set; }

        [JsonProperty("original_content_tag_enabled")]
        public bool OriginalContentTagEnabled { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("submit_link_label")]
        public string SubmitLinkLabel { get; set; }

        [JsonProperty("allow_post_crossposts")]
        public bool AllowPostCrossposts { get; set; }

        [JsonProperty("spam_comments")]
        public string SpamComments { get; set; }

        [JsonProperty("public_traffic")]
        public bool PublicTraffic { get; set; }

        [JsonProperty("submit_text_label")]
        public string SubmitTextLabel { get; set; }

        [JsonProperty("all_original_content")]
        public bool AllOriginalContent { get; set; }

        [JsonProperty("spam_selfposts")]
        public string SpamSelfPosts { get; set; }

        [JsonProperty("key_color")]
        public string KeyColor { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("wiki_edit_karma")]
        public int WikiEditKarma { get; set; }

        [JsonProperty("hide_ads")]
        public bool HideAds { get; set; }

        [JsonProperty("header_hover_text")]
        public string HeaderHoverText { get; set; }

        [JsonProperty("allow_disovery")]
        public bool AllowDiscovery { get; set; }

        [JsonProperty("public_description")]
        public string PublicDescription { get; set; }

        [JsonProperty("show_media_preview")]
        public bool ShowMediaPreview { get; set; }

        [JsonProperty("comment_score_hide_mins")]
        public int CommentScoreHideMins { get; set; }

        [JsonProperty("subreddit_type")]
        public string SubredditType { get; set; }

        [JsonProperty("exclude_banned_modqueue")]
        public bool ExcludeBannedModqueue { get; set; }

        [JsonProperty("content_options")]
        public string ContentOptions { get; set; }
    }
}
