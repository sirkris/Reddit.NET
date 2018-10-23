using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.NET.Models.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
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
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime Created;

        [JsonProperty("copied_from")]
        public string CopiedFrom;

        [JsonProperty("icon_url")]
        public string IconURL;

        [JsonProperty("subreddits")]
        public JArray Subreddits;

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(TimestampConvert))]
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
