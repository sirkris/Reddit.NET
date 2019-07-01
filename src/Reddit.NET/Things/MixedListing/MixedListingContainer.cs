using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class MixedListingContainer : BaseContainer
    {
        [JsonProperty("data")]
        public MixedListingData Data { get; set; }
    }
}
