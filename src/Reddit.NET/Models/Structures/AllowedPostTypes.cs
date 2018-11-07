using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
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
