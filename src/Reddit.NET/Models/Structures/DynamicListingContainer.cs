using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class DynamicListingContainer
    {
        [JsonProperty("kind")]
        public string Kind;

        [JsonProperty("data")]
        public DynamicListingData Data;
    }
}
