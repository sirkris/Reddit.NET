using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WikiPageRevisionData : BaseData
    {
        [JsonProperty("children")]
        public List<WikiPageRevision> Children;
    }
}
