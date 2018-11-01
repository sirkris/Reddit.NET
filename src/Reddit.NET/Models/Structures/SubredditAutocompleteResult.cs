using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
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
