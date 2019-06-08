using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class MuteStatus
    {
        [JsonProperty("isMuted")]
        public bool IsMuted;

        [JsonProperty("endDate")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime EndDate;

        [JsonProperty("reason")]
        public string Reason;
    }
}
