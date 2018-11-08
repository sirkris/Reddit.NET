using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class SubredditNames
    {
        [JsonProperty("names")]
        public List<string> Names;
    }
}
