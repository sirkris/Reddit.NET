using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class FlairRichtext
    {
        [JsonProperty("e")]
        public string E;

        [JsonProperty("t")]
        public string T;
    }
}
