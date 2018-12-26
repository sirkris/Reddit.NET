using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class SubredditData : BaseData
    {
        [JsonProperty("children")]
        public List<SubredditChild> Children;
    }
}
