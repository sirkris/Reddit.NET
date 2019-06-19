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
        public DateTime EndDate { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("isBanned")]
        public bool IsBanned { get; set; }

        [JsonProperty("isPermanent")]
        public bool IsPermanent { get; set; }
    }
}
