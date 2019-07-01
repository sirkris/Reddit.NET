using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WikiPageRevision
    {
        [JsonConverter(typeof(UtcTimestampConverter))]
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("author")]
        public UserChild Author { get; set; }

        [JsonProperty("page")]
        public string Page { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
