using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class BanStatus
    {
        [JsonProperty("endDate")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime EndDate;

        [JsonProperty("reason")]
        public string Reason;

        [JsonProperty("isBanned")]
        public bool IsBanned;

        [JsonProperty("isPermanent")]
        public bool IsPermanent;
    }
}
