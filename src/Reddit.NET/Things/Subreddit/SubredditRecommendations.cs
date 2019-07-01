using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class SubredditRecommendations
    {
        [JsonProperty("sr_name")]
        public string Name { get; set; }
    }
}
