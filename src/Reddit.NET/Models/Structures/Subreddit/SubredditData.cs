using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class SubredditData : BaseData
    {
        [JsonProperty("children")]
        public List<SubredditChild> Children;
    }
}
