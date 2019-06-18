using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class FlairSelectorResultContainer
    {
        [JsonProperty("current")]
        public FlairSelectorResult Current { get; set; }

        [JsonProperty("choices")]
        public List<FlairSelectorResult> Choices { get; set; }
    }
}
