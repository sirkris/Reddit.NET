using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class Snoomoji
    {
        [JsonProperty("url")]
        public string URL;

        [JsonProperty("created_by")]
        public string CreatedBy;
    }
}
