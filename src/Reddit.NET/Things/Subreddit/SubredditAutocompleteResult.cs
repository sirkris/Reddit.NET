using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class SubredditAutocompleteResult
    {
        [JsonProperty("numSubscribers")]
        public int NumSubscribers { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("allowedPostTypes")]
        public AllowedPostTypes AllowedPostTypes { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("primaryColor")]
        public string PrimaryColor { get; set; }

        [JsonProperty("communityIcon")]
        public string CommunityIcon { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
