using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ModActionData : BaseData
    {
        [JsonProperty("children")]
        public List<ModActionChild> Children;
    }
}
