using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class LabeledMulti
    {
        [JsonProperty("can_edit")]
        public bool CanEdit;

        [JsonProperty("display_name")]
        public string DisplayName;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description_html")]
        public string DescriptionHTML;

        [JsonProperty("created")]
        [JsonConverter(typeof(LocalTimestampConverter))]
        [Obsolete("Using this date can lead to unexpected results, please use " + nameof(CreatedUTC) + " instead.")]
        public DateTime Created;

        [JsonProperty("copied_from")]
        public string CopiedFrom;

        [JsonProperty("icon_url")]
        public string IconURL;

        [JsonProperty("subreddits")]
        public List<UserSubredditContainer> Subreddits;

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC;

        [JsonProperty("visibility")]
        public string Visibility;

        [JsonProperty("icon_name")]
        public string IconName;

        [JsonProperty("over_18")]
        public bool Over18;

        [JsonProperty("weighting_scheme")]
        public string WeightingScheme;

        [JsonProperty("path")]
        public string Path;

        [JsonProperty("key_color")]
        public string KeyColor;

        [JsonProperty("description_md")]
        public string DescriptionMd;
    }
}
