using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class ModmailSubreddit
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastUpdated")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("keyColor")]
        public string KeyColor { get; set; }

        [JsonProperty("subscribers")]
        public int Subscribers { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
