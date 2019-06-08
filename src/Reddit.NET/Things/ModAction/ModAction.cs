using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class ModAction
    {
        [JsonProperty("description")]
        public string Description;

        [JsonProperty("target_body")]
        public string TargetBody;

        [JsonProperty("mod_id36")]
        public string ModId36;

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC;

        [JsonProperty("subreddit")]
        public string Subreddit;

        [JsonProperty("target_title")]
        public string TargetTitle;

        [JsonProperty("target_permalink")]
        public string TargetPermalink;

        [JsonProperty("subreddit_name_prefixed")]
        public string SubredditNamePrefixed;

        [JsonProperty("details")]
        public string Details;

        [JsonProperty("action")]
        public string Action;

        [JsonProperty("target_author")]
        public string TargetAuthor;

        [JsonProperty("target_fullname")]
        public string TargetFullname;

        [JsonProperty("sr_id36")]
        public string SrId36;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("mod")]
        public string Mod;
    }
}
