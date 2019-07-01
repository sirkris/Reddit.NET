using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class MixedListingChild : BaseContainer
    {
        [JsonProperty("data")]
        public JObject Data { get; set; }
    }
}
