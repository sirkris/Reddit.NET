using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class SubredditSettings
    {
        [JsonProperty("default_set")]
        public bool DefaultSet;

        [JsonProperty("subreddit_id")]
        public string SubredditId;

        [JsonProperty("domain")]
        public string Domain;

        [JsonProperty("allow_images")]
        public bool AllowImages;

        [JsonProperty("free_form_reports")]
        public bool FreeFormReports;

        [JsonProperty("show_media")]
        public bool ShowMedia;

        [JsonProperty("wiki_edit_age")]
        public int WikiEditAge;

        [JsonProperty("submit_text")]
        public string SubmitText;

        [JsonProperty("spam_links")]
        public string SpamLinks;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("collapse_deleted_comments")]
        public bool CollapseDeletedComments;

        [JsonProperty("wikimode")]
        public string WikiMode;

        [JsonProperty("over_18")]
        public bool Over18;

        [JsonProperty("allow_videos")]
        public bool AllowVideos;

        [JsonProperty("spoilers_enabled")]
        public bool SpoilersEnabled;

        [JsonProperty("suggested_comment_sort")]
        public string SuggestedCommentSort;

        [JsonProperty("original_content_tag_enabled")]
        public bool OriginalContentTagEnabled;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("submit_link_label")]
        public string SubmitLinkLabel;

        [JsonProperty("allow_post_crossposts")]
        public bool AllowPostCrossposts;

        [JsonProperty("spam_comments")]
        public string SpamComments;

        [JsonProperty("public_traffic")]
        public bool PublicTraffic;

        [JsonProperty("submit_text_label")]
        public string SubmitTextLabel;

        [JsonProperty("all_original_content")]
        public bool AllOriginalContent;

        [JsonProperty("spam_selfposts")]
        public string SpamSelfPosts;

        [JsonProperty("key_color")]
        public string KeyColor;

        [JsonProperty("language")]
        public string Language;

        [JsonProperty("wiki_edit_karma")]
        public int WikiEditKarma;

        [JsonProperty("hide_ads")]
        public bool HideAds;

        [JsonProperty("header_hover_text")]
        public string HeaderHoverText;

        [JsonProperty("allow_disovery")]
        public bool AllowDiscovery;

        [JsonProperty("public_description")]
        public string PublicDescription;

        [JsonProperty("show_media_preview")]
        public bool ShowMediaPreview;

        [JsonProperty("comment_score_hide_mins")]
        public int CommentScoreHideMins;

        [JsonProperty("subreddit_type")]
        public string SubredditType;

        [JsonProperty("exclude_banned_modqueue")]
        public bool ExcludeBannedModqueue;

        [JsonProperty("content_options")]
        public string ContentOptions;
    }
}
