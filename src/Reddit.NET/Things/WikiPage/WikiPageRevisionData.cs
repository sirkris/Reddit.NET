using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class WikiPageRevisionData : BaseData
    {
        [JsonProperty("children")]
        public List<WikiPageRevision> Children { get; set; }
    }
}
