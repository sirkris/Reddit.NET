using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class DynamicListingData : BaseData
    {
        [JsonProperty("children")]
        public List<DynamicListingChild> Children;
    }
}
