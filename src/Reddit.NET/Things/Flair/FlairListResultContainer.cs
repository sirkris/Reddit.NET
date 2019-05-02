using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class FlairListResultContainer
    {
        [JsonProperty("prev")]
        public string prev;

        [JsonProperty("next")]
        public string next;

        [JsonProperty("users")]
        public List<FlairListResult> Users;

        public bool HasMore()
        {
            return !string.IsNullOrEmpty(next);
        }
    }
}
