using Newtonsoft.Json;
using Reddit.NET.Models.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class UserActionResult
    {
        [JsonProperty("date")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime Date;

        [JsonProperty("icon_img")]
        public string IconImg;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;
    }
}
