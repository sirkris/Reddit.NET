using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class Award
    {
        [JsonProperty("icon_70")]
        public string Icon70 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("icon_40")]
        public string Icon40 { get; set; }

        [JsonProperty("award_id")]
        public string AwardId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
