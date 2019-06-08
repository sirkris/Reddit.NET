using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class Convo
    {
        [JsonProperty("date")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime Date;

        [JsonProperty("permalink")]
        public string Permalink;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("subject")]
        public string Subject;
    }
}
