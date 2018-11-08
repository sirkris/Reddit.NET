using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class FlairListResultContainer
    {
        [JsonProperty("users")]
        public List<FlairListResult> Users;
    }
}
