using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WikiPageRevisionData
    {
        [JsonProperty("modhash")]
        public string Modhash;

        [JsonProperty("dist")]
        public int? Dist;

        [JsonProperty("children")]
        public List<WikiPageRevision> Children;

        [JsonProperty("after")]
        public string after;

        [JsonProperty("before")]
        public string before;
    }
}
