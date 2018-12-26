using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class MuteStatus
    {
        [JsonProperty("isMuted")]
        public bool IsMuted;

        [JsonProperty("endDate")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime EndDate;

        [JsonProperty("reason")]
        public string Reason;
    }
}
