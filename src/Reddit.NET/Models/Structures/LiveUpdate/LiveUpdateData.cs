using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class LiveUpdateData : BaseData
    {
        [JsonProperty("children")]
        public List<LiveUpdateChild> Children;
    }
}
