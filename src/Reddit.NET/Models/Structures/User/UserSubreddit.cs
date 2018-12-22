using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class UserSubreddit
    {
        [JsonProperty("default_set")]
        public bool DefaultSet;

        [JsonProperty("banner_img")]
        public string BannerImg;

        [JsonProperty("user_is_banned")]
        public bool UserIsBanned;

        [JsonProperty("free_form_reports")]
        public bool FreeFormReports;

        [JsonProperty("community_icon")]
        public string CommunityIcon;

        [JsonProperty("show_media")]
        public bool ShowMedia;

        [JsonProperty("icon_color")]
        public string IconColor;

        [JsonProperty("user_is_muted")]
        public bool UserIsMuted;

        [JsonProperty("display_name")]
        public string DisplayName;

        [JsonProperty("header_img")]
        public string HeaderImg;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("user_is_moderator")]
        public bool UserIsModerator;

        [JsonProperty("over_18")]
        public bool Over18;

        [JsonProperty("icon_size")]
        public List<int> IconSize;

        [JsonProperty("primary_color")]
        public string PrimaryColor;

        [JsonProperty("audience_target")]
        public string AudienceTarget;

        [JsonProperty("icon_img")]
        public string IconImg;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("header_size")]
        public List<int> HeaderSize;

        [JsonProperty("subscribers")]
        public int Subscribers;

        [JsonProperty("is_default_icon")]
        public bool IsDefaultIcon;

        [JsonProperty("link_flair_position")]
        public string LinkFlairPosition;

        [JsonProperty("display_name_prefixed")]
        public string DisplayNamePrefixed;

        [JsonProperty("key_color")]
        public string KeyColor;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("is_default_banner")]
        public string IsDefaultBanner;

        [JsonProperty("url")]
        public string URL;

        [JsonProperty("banner_size")]
        public List<int> BannerSize;

        [JsonProperty("user_is_contributor")]
        public bool UserIsContributor;

        [JsonProperty("public_description")]
        public string PublicDescription;

        [JsonProperty("link_flair_enabled")]
        public bool LinkFlairEnabled;

        [JsonProperty("subreddit_type")]
        public string SubredditType;

        [JsonProperty("user_is_subscriber")]
        public bool UserIsSubscriber;
    }
}
