using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class DynamicListingContainer : BaseContainer
    {
        [JsonProperty("data")]
        public DynamicListingData Data;
    }
}
