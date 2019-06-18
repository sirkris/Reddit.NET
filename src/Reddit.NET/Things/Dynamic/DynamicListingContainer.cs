using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class DynamicListingContainer : BaseContainer
    {
        [JsonProperty("data")]
        public DynamicListingData Data { get; set; }
    }
}
