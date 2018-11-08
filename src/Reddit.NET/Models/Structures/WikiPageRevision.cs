using Newtonsoft.Json;
using Reddit.NET.Models.Converters;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WikiPageRevision
    {
        [JsonConverter(typeof(TimestampConvert))]
        [JsonProperty("timestamp")]
        public DateTime Timestamp;

        [JsonProperty("reason")]
        public string Reason;

        [JsonProperty("author")]
        public UserChild Author;

        [JsonProperty("page")]
        public string Page;

        [JsonProperty("id")]
        public string Id;
    }
}
