using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class LiveUpdateData : BaseData
    {
        [JsonProperty("children")]
        public List<LiveUpdateChild> Children { get; set; }
    }
}
