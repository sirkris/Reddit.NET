using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WikiPageListing : BaseContainer
    {
        [JsonProperty("data")]
        public List<string> Data;
    }
}
