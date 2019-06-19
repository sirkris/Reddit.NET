using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class MuteStatus
    {
        [JsonProperty("isMuted")]
        public bool IsMuted { get; set; }

        [JsonProperty("endDate")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime EndDate { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
