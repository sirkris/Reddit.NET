using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class SubredditAutocompleteResult
    {
        [JsonProperty("numSubscribers")]
        public int NumSubscribers;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("allowedPostTypes")]
        public AllowedPostTypes AllowedPostTypes;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("primaryColor")]
        public string PrimaryColor;

        [JsonProperty("communityIcon")]
        public string CommunityIcon;

        [JsonProperty("icon")]
        public string Icon;
    }
}
