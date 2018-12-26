using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class AllowedPostTypes
    {
        [JsonProperty("images")]
        public bool Images;

        [JsonProperty("text")]
        public bool Text;

        [JsonProperty("videos")]
        public bool Videos;

        [JsonProperty("links")]
        public bool Links;

        [JsonProperty("spoilers")]
        public bool Spoilers;
    }
}
