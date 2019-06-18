using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class PostResultShortData
    {
        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("drafts_count")]
        public int DraftsCount { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
