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
        public bool CanEdit { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description_html")]
        public string DescriptionHTML { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(LocalTimestampConverter))]
        [Obsolete("Using this date can lead to unexpected results.  It is recommended that you use " + nameof(CreatedUTC) + " instead.")]
        public DateTime Created { get; set; }

        [JsonProperty("copied_from")]
        public string CopiedFrom { get; set; }

        [JsonProperty("icon_url")]
        public string IconURL { get; set; }

        [JsonProperty("subreddits")]
        public List<UserSubredditContainer> Subreddits { get; set; }

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("icon_name")]
        public string IconName { get; set; }

        [JsonProperty("over_18")]
        public bool Over18 { get; set; }

        [JsonProperty("weighting_scheme")]
        public string WeightingScheme { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("key_color")]
        public string KeyColor { get; set; }

        [JsonProperty("description_md")]
        public string DescriptionMd { get; set; }
    }
}
