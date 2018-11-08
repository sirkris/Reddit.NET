using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class FlairSelectorResultContainer
    {
        [JsonProperty("current")]
        public FlairSelectorResult Current;

        [JsonProperty("choices")]
        public List<FlairSelectorResult> Choices;
    }
}
