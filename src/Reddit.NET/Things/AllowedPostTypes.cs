using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class AllowedPostTypes
    {
        [JsonProperty("images")]
        public bool Images { get; set; }

        [JsonProperty("text")]
        public bool Text { get; set; }

        [JsonProperty("videos")]
        public bool Videos { get; set; }

        [JsonProperty("links")]
        public bool Links { get; set; }

        [JsonProperty("spoilers")]
        public bool Spoilers { get; set; }
    }
}
