using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class FlairListResultContainer
    {
        [JsonProperty("prev")]
        public string Prev;

        [JsonProperty("users")]
        public List<FlairListResult> Users;

        [JsonProperty("next")]
        public string Next;
    }
}
