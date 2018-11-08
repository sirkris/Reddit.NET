using Newtonsoft.Json;
using Reddit.NET.Models.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
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
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime CreatedUTC;

        [JsonProperty("priority")]
        public int Priority;

        [JsonProperty("description_html")]
        public string DescriptionHTML;
    }
}
