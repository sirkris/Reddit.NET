using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Coordinators.Structures
{
    [Serializable]
    public class SubredditUser
    {
        [JsonProperty("date")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime Date;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("id")]
        public string Id;
    }
}
