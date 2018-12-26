using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class DynamicShortListingData : BaseData
    {
        [JsonProperty("children")]
        public List<dynamic> Children;
    }
}
