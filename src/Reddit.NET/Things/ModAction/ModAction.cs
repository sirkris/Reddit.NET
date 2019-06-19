using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class ModAction
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("target_body")]
        public string TargetBody { get; set; }

        [JsonProperty("mod_id36")]
        public string ModId36 { get; set; }

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC { get; set; }

        [JsonProperty("subreddit")]
        public string Subreddit { get; set; }

        [JsonProperty("target_title")]
        public string TargetTitle { get; set; }

        [JsonProperty("target_permalink")]
        public string TargetPermalink { get; set; }

        [JsonProperty("subreddit_name_prefixed")]
        public string SubredditNamePrefixed { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("target_author")]
        public string TargetAuthor { get; set; }

        [JsonProperty("target_fullname")]
        public string TargetFullname { get; set; }

        [JsonProperty("sr_id36")]
        public string SrId36 { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("mod")]
        public string Mod { get; set; }
    }
}
