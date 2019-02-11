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
        public string NotificationLevel;

        [JsonProperty("user_flair_background_color")]
        public string UserFlairBackgroundColor;

        [JsonProperty("wls")]
        public int? Wls;

        [JsonProperty("banner_img")]
        public string BannerImg;

        [JsonProperty("user_sr_theme_enabled")]
        public bool? UserSrThemeEnabled;

        [JsonProperty("user_sr_flair_enabled")]
        public bool? UserSrFlairEnabled;

        [JsonProperty("user_flair_text")]
        public string UserFlairText;

        [JsonProperty("submit_text_html")]
        public string SubmitTextHTML;

        [JsonProperty("user_flair_css_class")]
        public string UserFlairCssClass;

        [JsonProperty("user_flair_template_id")]
        public string UserFlairTemplateId;

        [JsonProperty("user_is_banned")]
        public bool? UserIsBanned;

        [JsonProperty("subreddit_type")]
        public string SubredditType;

        [JsonProperty("community_icon")]
        public string CommunityIcon;

        [JsonProperty("banner_background_image")]
        public string BannerBackgroundImage;

        [JsonProperty("header_title")]
        public string HeaderTitle;

        [JsonProperty("wiki_enabled")]
        public bool? WikiEnabled;

        [JsonProperty("over18")]
        public bool? Over18;

        [JsonProperty("show_media")]
        public bool? ShowMedia;

        [JsonProperty("banner_background_color")]
        public string BannerBackgroundColor;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("user_is_muted")]
        public bool? UserIsMuted;

        [JsonProperty("user_flair_type")]
        public string UserFlairType;

        [JsonProperty("user_can_flair_in_sr")]
        public bool? UserCanFlairInSr;

        [JsonProperty("display_name")]
        public string DisplayName;

        [JsonProperty("header_img")]
        public object HeaderImg;

        [JsonProperty("description_html")]
        public string DescriptionHTML;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("collapse_deleted_comments")]
        public bool? CollapseDeletedComments;

        [JsonProperty("user_has_favorited")]
        public bool? UserHasFavorited;

        [JsonProperty("emojis_custom_size")]
        public object EmojisCustomSize;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("emojis_enabled")]
        public bool EmojisEnabled;

        [JsonProperty("public_description_html")]
        public string PublicDescriptionHTML;

        [JsonProperty("can_assign_user_flair")]
        public bool CanAssignUserFlair;

        [JsonProperty("allow_videos")]
        public bool AllowVideos;

        [JsonProperty("spoilers_enabled")]
        public bool? SpoilersEnabled;

        [JsonProperty("icon_size")]
        public List<int> IconSize;

        [JsonProperty("primary_color")]
        public string PrimaryColor;

        [JsonProperty("user_is_contributor")]
        public bool? UserIsContributor;

        [JsonProperty("audience_target")]
        public string AudienceTarget;

        [JsonProperty("suggested_comment_sort")]
        public string SuggestedCommentSort;

        [JsonProperty("active_user_count")]
        public int? ActiveUserCount;

        [JsonProperty("icon_img")]
        public object IconImg;

        [JsonProperty("original_content_tag_enabled")]
        public bool? OriginalContentTagEnabled;

        [JsonProperty("display_name_prefixed")]
        public string DisplayNamePrefixed;

        [JsonProperty("can_assign_link_flair")]
        public bool CanAssignLinkFlair;

        [JsonProperty("submit_text")]
        public string SubmitText;

        [JsonProperty("allow_videogifs")]
        public bool AllowVideoGifs;

        [JsonProperty("user_flair_text_color")]
        public string UserFlairTextColor;

        [JsonProperty("accounts_active")]
        public int? AccountsActive;

        [JsonProperty("public_traffic")]
        public bool? PublicTraffic;

        [JsonProperty("header_size")]
        public List<int> HeaderSize;

        [JsonProperty("subscribers")]
        public int? Subscribers;

        [JsonProperty("user_flair_position")]
        public string UserFlairPosition;

        [JsonProperty("submit_text_label")]
        public string SubmitTextLabel;

        [JsonProperty("key_color")]
        public string KeyColor;

        [JsonProperty("link_flair_position")]
        public string LinkFlairPosition;

        [JsonProperty("user_flair_richtext")]
        public object UserFlairRichtext;

        [JsonProperty("all_original_content")]
        public bool? AllOriginalContent;

        [JsonProperty("lang")]
        public string Lang;

        [JsonProperty("has_menu_widget")]
        public bool HasMenuWidget;

        [JsonProperty("is_enrolled_in_new_modmail")]
        public bool? IsEnrolledInNewModmail;

        [JsonProperty("whitelist_status")]
        public string WhitelistStatus;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("user_flair_enabled_in_sr")]
        public bool? UserFlairEnabledInSr;

        [JsonProperty("created")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime Created;

        [JsonProperty("url")]
        public string URL;

        [JsonProperty("quarantine")]
        public bool? Quarantine;

        [JsonProperty("hide_ads")]
        public bool? HideAds;

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime CreatedUTC;

        [JsonProperty("banner_size")]
        public List<int> BannerSize;

        [JsonProperty("user_is_moderator")]
        public bool? UserIsModerator;

        [JsonProperty("submit_link_label")]
        public string SubmitLinkLabel;

        [JsonProperty("allow_discovery")]
        public bool? AllowDiscovery;

        [JsonProperty("accounts_active_is_fuzzed")]
        public bool? AccountsActiveIsFuzzed;

        [JsonProperty("advertiser_category")]
        public string AdvertiserCategory;

        [JsonProperty("public_description")]
        public string PublicDescription;

        [JsonProperty("link_flair_enabled")]
        public bool? LinkFlairEnabled;

        [JsonProperty("allow_images")]
        public bool? AllowImages;

        [JsonProperty("videostream_links_count")]
        public int VideoStreamLinksCount;

        [JsonProperty("comment_score_hide_mins")]
        public int? CommentScoreHideMins;

        [JsonProperty("show_media_preview")]
        public bool? ShowMediaPreview;

        [JsonProperty("submission_type")]
        public string SubmissionType;

        [JsonProperty("user_is_subscriber")]
        public bool? UserIsSubscriber;

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
            Created = subreddit.Created;
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
