using Newtonsoft.Json;
using Reddit.NET.Models.Converters;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class UserSummary
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime CreatedUTC;

        [JsonProperty("link_karma")]
        public int LinkKarma;

        [JsonProperty("comment_karma")]
        public int CommentKarma;

        [JsonProperty("profile_img")]
        public string ProfileImg;

        [JsonProperty("profile_color")]
        public string ProfileColor;

        [JsonProperty("profile_over_18")]
        public bool ProfileOver18;
    }
}
