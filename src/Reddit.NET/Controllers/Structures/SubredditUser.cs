using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Controllers.Structures
{
    [Serializable]
    public class SubredditUser
    {
        [JsonProperty("date")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime Date;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("id")]
        public string Id;
    }
}
