using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class ModeratedListItem
    {
        [JsonProperty("icon_img")]
        public string IconImg { get; set; }

        [JsonProperty("banner_img")]
        public string BannerImg { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("primary_color")]
        public string PrimaryColor { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("sr")]
        public string SR { get; set; }

        [JsonProperty("sr_display_name_prefixed")]
        public string SRDisplayNamePrefixed { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        [Obsolete("Using this date can lead to unexpected results.  It is recommended that you use " + nameof(CreatedUTC) + " instead.")]
        public DateTime Created { get; set; }

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC { get; set; }

        [JsonProperty("banner_size")]
        public List<int> BannerSize { get; set; }

        [JsonProperty("icon_size")]
        public List<int> IconSize { get; set; }

        [JsonProperty("mod_permissions")]
        public List<string> ModPermissions { get; set; }

        [JsonProperty("over_18")]
        public bool Over18 { get; set; }

        [JsonProperty("subscribers")]
        public int Subscribers { get; set; }

        [JsonProperty("community_icon")]
        public string CommunityIcon { get; set; }

        [JsonProperty("subreddit_type")]
        public string SubredditType { get; set; }

        [JsonProperty("key_color")]
        public string KeyColor { get; set; }

        [JsonProperty("user_can_crosspost")]
        public bool UserCanCrosspost { get; set; }

        // TODO - Determine type.  --Kris
        [JsonProperty("whitelist_status")]
        public object WhitelistStatus { get; set; }

        [JsonProperty("user_is_subscriber")]
        public bool UserIsSubscriber { get; set; }
    }
}
