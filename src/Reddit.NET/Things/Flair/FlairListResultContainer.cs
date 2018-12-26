using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class FlairListResultContainer
    {
        [JsonProperty("users")]
        public List<FlairListResult> Users;
    }
}
