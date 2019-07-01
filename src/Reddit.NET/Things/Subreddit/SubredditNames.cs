using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class SubredditNames
    {
        [JsonProperty("names")]
        public List<string> Names { get; set; }
    }
}
