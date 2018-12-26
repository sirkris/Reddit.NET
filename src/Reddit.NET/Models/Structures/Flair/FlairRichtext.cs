using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
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
