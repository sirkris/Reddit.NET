using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WikiPageRevisionData : BaseData
    {
        [JsonProperty("children")]
        public List<WikiPageRevision> Children;
    }
}
