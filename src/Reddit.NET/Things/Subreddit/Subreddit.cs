using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class Subreddit
    {
        [JsonProperty("notification_level")]
        public string NotificationLevel { get; set; }

        [JsonProperty("user_flair_background_color")]
        public string UserFlairBackgroundColor { get; set; }

        [JsonProperty("wls")]
        public int? Wls { get; set; }

        [JsonProperty("banner_img")]
        public string BannerImg { get; set; }

        [JsonProperty("user_sr_theme_enabled")]
        public bool? UserSrThemeEnabled { get; set; }

        [JsonProperty("user_sr_flair_enabled")]
        public bool? UserSrFlairEnabled { get; set; }

        [JsonProperty("user_flair_text")]
        public string UserFlairText { get; set; }

        [JsonProperty("submit_text_html")]
        public string SubmitTextHTML { get; set; }

        [JsonProperty("user_flair_css_class")]
        public string UserFlairCssClass { get; set; }

        [JsonProperty("user_flair_template_id")]
        public string UserFlairTemplateId { get; set; }

        [JsonProperty("user_is_banned")]
        public bool? UserIsBanned { get; set; }

        [JsonProperty("subreddit_type")]
        public string SubredditType { get; set; }

        [JsonProperty("community_icon")]
        public string CommunityIcon { get; set; }

        [JsonProperty("banner_background_image")]
        public string BannerBackgroundImage { get; set; }

        [JsonProperty("header_title")]
        public string HeaderTitle { get; set; }

        [JsonProperty("wiki_enabled")]
        public bool? WikiEnabled { get; set; }

        [JsonProperty("over18")]
        public bool? Over18 { get; set; }

        [JsonProperty("show_media")]
        public bool? ShowMedia { get; set; }

        [JsonProperty("banner_background_color")]
        public string BannerBackgroundColor { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("user_is_muted")]
        public bool? UserIsMuted { get; set; }

        [JsonProperty("user_flair_type")]
        public string UserFlairType { get; set; }

        [JsonProperty("user_can_flair_in_sr")]
        public bool? UserCanFlairInSr { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("header_img")]
        public object HeaderImg { get; set; }

        [JsonProperty("description_html")]
        public string DescriptionHTML { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("collapse_deleted_comments")]
        public bool? CollapseDeletedComments { get; set; }

        [JsonProperty("user_has_favorited")]
        public bool? UserHasFavorited { get; set; }

        [JsonProperty("emojis_custom_size")]
        public object EmojisCustomSize { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("emojis_enabled")]
        public bool EmojisEnabled { get; set; }

        [JsonProperty("public_description_html")]
        public string PublicDescriptionHTML { get; set; }

        [JsonProperty("can_assign_user_flair")]
        public bool CanAssignUserFlair { get; set; }

        [JsonProperty("allow_videos")]
        public bool AllowVideos { get; set; }

        [JsonProperty("spoilers_enabled")]
        public bool? SpoilersEnabled { get; set; }

        [JsonProperty("icon_size")]
        public List<int> IconSize { get; set; }

        [JsonProperty("primary_color")]
        public string PrimaryColor { get; set; }

        [JsonProperty("user_is_contributor")]
        public bool? UserIsContributor { get; set; }

        [JsonProperty("audience_target")]
        public string AudienceTarget { get; set; }

        [JsonProperty("suggested_comment_sort")]
        public string SuggestedCommentSort { get; set; }

        [JsonProperty("active_user_count")]
        public int? ActiveUserCount { get; set; }

        [JsonProperty("icon_img")]
        public object IconImg { get; set; }

        [JsonProperty("original_content_tag_enabled")]
        public bool? OriginalContentTagEnabled { get; set; }

        [JsonProperty("display_name_prefixed")]
        public string DisplayNamePrefixed { get; set; }

        [JsonProperty("can_assign_link_flair")]
        public bool CanAssignLinkFlair { get; set; }

        [JsonProperty("submit_text")]
        public string SubmitText { get; set; }

        [JsonProperty("allow_videogifs")]
        public bool AllowVideoGifs { get; set; }

        [JsonProperty("user_flair_text_color")]
        public string UserFlairTextColor { get; set; }

        [JsonProperty("accounts_active")]
        public int? AccountsActive { get; set; }

        [JsonProperty("public_traffic")]
        public bool? PublicTraffic { get; set; }

        [JsonProperty("header_size")]
        public List<int> HeaderSize { get; set; }

        [JsonProperty("subscribers")]
        public int? Subscribers { get; set; }

        [JsonProperty("user_flair_position")]
        public string UserFlairPosition { get; set; }

        [JsonProperty("submit_text_label")]
        public string SubmitTextLabel { get; set; }

        [JsonProperty("key_color")]
        public string KeyColor { get; set; }

        [JsonProperty("link_flair_position")]
        public string LinkFlairPosition { get; set; }

        [JsonProperty("user_flair_richtext")]
        public object UserFlairRichtext { get; set; }

        [JsonProperty("all_original_content")]
        public bool? AllOriginalContent { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("has_menu_widget")]
        public bool HasMenuWidget { get; set; }

        [JsonProperty("is_enrolled_in_new_modmail")]
        public bool? IsEnrolledInNewModmail { get; set; }

        [JsonProperty("whitelist_status")]
        public string WhitelistStatus { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("user_flair_enabled_in_sr")]
        public bool? UserFlairEnabledInSr { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(LocalTimestampConverter))]
        [Obsolete("Using this date can lead to unexpected results.  It is recommended that you use " + nameof(CreatedUTC) + " instead.")]
        public DateTime Created { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("quarantine")]
        public bool? Quarantine { get; set; }

        [JsonProperty("hide_ads")]
        public bool? HideAds { get; set; }

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC { get; set; }

        [JsonProperty("banner_size")]
        public List<int> BannerSize { get; set; }

        [JsonProperty("user_is_moderator")]
        public bool? UserIsModerator { get; set; }

        [JsonProperty("submit_link_label")]
        public string SubmitLinkLabel { get; set; }

        [JsonProperty("allow_discovery")]
        public bool? AllowDiscovery { get; set; }

        [JsonProperty("accounts_active_is_fuzzed")]
        public bool? AccountsActiveIsFuzzed { get; set; }

        [JsonProperty("advertiser_category")]
        public string AdvertiserCategory { get; set; }

        [JsonProperty("public_description")]
        public string PublicDescription { get; set; }

        [JsonProperty("link_flair_enabled")]
        public bool? LinkFlairEnabled { get; set; }

        [JsonProperty("allow_images")]
        public bool? AllowImages { get; set; }

        [JsonProperty("videostream_links_count")]
        public int VideoStreamLinksCount { get; set; }

        [JsonProperty("comment_score_hide_mins")]
        public int? CommentScoreHideMins { get; set; }

        [JsonProperty("show_media_preview")]
        public bool? ShowMediaPreview { get; set; }

        [JsonProperty("submission_type")]
        public string SubmissionType { get; set; }

        [JsonProperty("user_is_subscriber")]
        public bool? UserIsSubscriber { get; set; }

        [JsonProperty("event_posts_enabled")]
        public bool? EventPostsEnabled { get; set; }

        [JsonProperty("mod_permissions")]
        public List<string> ModPermissions { get; set; }

        public Subreddit(Controllers.Subreddit subreddit)
        {
            BannerImg = subreddit.BannerImg;
            BannerBackgroundColor = subreddit.BannerBackgroundColor;
            BannerBackgroundImage = subreddit.BannerBackgroundImage;
            SubredditType = subreddit.SubredditType;
            CommunityIcon = subreddit.CommunityIcon;
            HeaderTitle = subreddit.HeaderTitle;
            WikiEnabled = subreddit.WikiEnabled;
            Over18 = subreddit.Over18;
            Description = subreddit.Sidebar;
            DisplayName = subreddit.Name;
            HeaderImg = subreddit.HeaderImg;
            Title = subreddit.Title;
            CollapseDeletedComments = subreddit.CollapseDeletedComments;
            Id = subreddit.Id;
            EmojisEnabled = subreddit.EmojisEnabled;
            ShowMedia = subreddit.ShowMedia;
            AllowVideos = subreddit.AllowVideos;
            CanAssignUserFlair = subreddit.CanAssignUserFlair;
            SpoilersEnabled = subreddit.SpoilersEnabled;
            PrimaryColor = subreddit.PrimaryColor;
            SuggestedCommentSort = subreddit.SuggestedCommentSort;
            ActiveUserCount = subreddit.ActiveUserCount;
            IconImg = subreddit.IconImg;
            CanAssignLinkFlair = subreddit.CanAssignLinkFlair;
            SubmitText = subreddit.submitText.SubmitText;
            AllowVideoGifs = subreddit.AllowVideoGifs;
            Subscribers = subreddit.Subscribers;
            SubmitTextLabel = subreddit.SubmitTextLabel;
            KeyColor = subreddit.KeyColor;
            Lang = subreddit.Lang;
            Name = subreddit.Fullname;
            CreatedUTC = subreddit.Created;
            URL = subreddit.URL;
            SubmitLinkLabel = subreddit.SubmitLinkLabel;
            AllowDiscovery = subreddit.AllowDiscovery;
            PublicDescription = subreddit.Description;
            LinkFlairEnabled = subreddit.LinkFlairEnabled;
            AllowImages = subreddit.AllowImages;
            CommentScoreHideMins = subreddit.CommentScoreHideMins;
            ShowMediaPreview = subreddit.ShowMediaPreview;
            SubmissionType = subreddit.SubmissionType;
        }

        public Subreddit() { }
    }
}
