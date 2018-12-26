using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class DynamicListingData : BaseData
    {
        [JsonProperty("children")]
        public List<DynamicListingChild> Children;
    }
}
