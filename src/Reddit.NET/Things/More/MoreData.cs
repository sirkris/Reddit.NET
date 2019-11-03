using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class MoreData : BaseData
    {
        [JsonProperty("children")]
        public List<MoreChild> Children { get; set; }
    }
}
