using Newtonsoft.Json;
using Reddit.NET.Models.Converters;
using System;

namespace Reddit.NET.Models.Structures
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
