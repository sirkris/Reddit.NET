using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class DynamicShortListingContainer : BaseContainer
    {
        [JsonProperty("data")]
        public DynamicShortListingData Data { get; set; }
    }
}
