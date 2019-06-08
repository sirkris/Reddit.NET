using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Controllers.Structures
{
    [Serializable]
    public class BannedUser
    {
        [JsonProperty("date")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime Date;

        [JsonProperty("note")]
        public string Note;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("id")]
        public string Id;
    }
}
