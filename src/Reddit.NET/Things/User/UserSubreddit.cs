using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class UserSubreddit
    {
        [JsonProperty("default_set")]
        public bool DefaultSet { get; set; }

        [JsonProperty("banner_img")]
        public string BannerImg { get; set; }

        [JsonProperty("user_is_banned")]
        public bool UserIsBanned { get; set; }

        [JsonProperty("free_form_reports")]
        public bool FreeFormReports { get; set; }

        [JsonProperty("community_icon")]
        public string CommunityIcon { get; set; }

        [JsonProperty("show_media")]
        public bool ShowMedia { get; set; }

        [JsonProperty("icon_color")]
        public string IconColor { get; set; }

        [JsonProperty("user_is_muted")]
        public bool UserIsMuted { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("header_img")]
        public string HeaderImg { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("user_is_moderator")]
        public bool UserIsModerator { get; set; }

        [JsonProperty("over_18")]
        public bool Over18 { get; set; }

        [JsonProperty("icon_size")]
        public List<int> IconSize { get; set; }

        [JsonProperty("primary_color")]
        public string PrimaryColor { get; set; }

        [JsonProperty("audience_target")]
        public string AudienceTarget { get; set; }

        [JsonProperty("icon_img")]
        public string IconImg { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("header_size")]
        public List<int> HeaderSize { get; set; }

        [JsonProperty("subscribers")]
        public int Subscribers { get; set; }

        [JsonProperty("is_default_icon")]
        public bool IsDefaultIcon { get; set; }

        [JsonProperty("link_flair_position")]
        public string LinkFlairPosition { get; set; }

        [JsonProperty("display_name_prefixed")]
        public string DisplayNamePrefixed { get; set; }

        [JsonProperty("key_color")]
        public string KeyColor { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_default_banner")]
        public string IsDefaultBanner { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("banner_size")]
        public List<int> BannerSize { get; set; }

        [JsonProperty("user_is_contributor")]
        public bool UserIsContributor { get; set; }

        [JsonProperty("public_description")]
        public string PublicDescription { get; set; }

        [JsonProperty("link_flair_enabled")]
        public bool LinkFlairEnabled { get; set; }

        [JsonProperty("subreddit_type")]
        public string SubredditType { get; set; }

        [JsonProperty("user_is_subscriber")]
        public bool UserIsSubscriber { get; set; }
    }
}
