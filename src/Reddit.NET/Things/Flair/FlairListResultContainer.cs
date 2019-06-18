using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class FlairListResultContainer
    {
        [JsonProperty("prev")]
        public string Prev { get; set; }

        [JsonProperty("users")]
        public List<FlairListResult> Users { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }
}
