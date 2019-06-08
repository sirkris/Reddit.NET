using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class UserActionResult
    {
        [JsonProperty("date")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime Date;

        [JsonProperty("icon_img")]
        public string IconImg;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;
    }
}
