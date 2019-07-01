using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class DynamicListingChild : BaseContainer
    {
        [JsonProperty("data")]
        public dynamic Data { get; set; }
    }
}
