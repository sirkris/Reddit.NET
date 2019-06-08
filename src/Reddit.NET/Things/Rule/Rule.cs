using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class Rule : BaseContainer
    {
        [JsonProperty("description")]
        public string Description;

        [JsonProperty("short_name")]
        public string ShortName;

        [JsonProperty("violation_reason")]
        public string ViolationReason;

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC;

        [JsonProperty("priority")]
        public int Priority;

        [JsonProperty("description_html")]
        public string DescriptionHTML;
    }
}
