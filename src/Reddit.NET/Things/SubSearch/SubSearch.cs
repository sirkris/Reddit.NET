using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class SubSearch
    {
        [JsonProperty("subreddits")]
        public List<SubSearchResult> Subreddits { get; set; }
    }
}
