using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class Rule : BaseContainer
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("violation_reason")]
        public string ViolationReason { get; set; }

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("description_html")]
        public string DescriptionHTML { get; set; }
    }
}
