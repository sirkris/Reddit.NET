using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class UserSummary
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC { get; set; }

        [JsonProperty("link_karma")]
        public int LinkKarma { get; set; }

        [JsonProperty("comment_karma")]
        public int CommentKarma { get; set; }

        [JsonProperty("profile_img")]
        public string ProfileImg { get; set; }

        [JsonProperty("profile_color")]
        public string ProfileColor { get; set; }

        [JsonProperty("profile_over_18")]
        public bool ProfileOver18 { get; set; }
    }
}
