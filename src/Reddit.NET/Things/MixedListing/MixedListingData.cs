using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class MixedListingData : BaseData
    {
        [JsonProperty("children")]
        public List<MixedListingChild> Children;

        [JsonProperty("facets")]
        public object Facets;  // TODO - Find out what this is.  It's used by Models.Search.GetSearch.  Comes up empty in tests even when include_facets is true.  --Kris
    }
}
