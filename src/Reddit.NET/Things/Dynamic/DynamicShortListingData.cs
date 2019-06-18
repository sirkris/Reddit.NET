using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class DynamicShortListingData : BaseData
    {
        [JsonProperty("children")]
        public List<dynamic> Children { get; set; }
    }
}
