using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class ModActionData : BaseData
    {
        [JsonProperty("children")]
        public List<ModActionChild> Children { get; set; }
    }
}
