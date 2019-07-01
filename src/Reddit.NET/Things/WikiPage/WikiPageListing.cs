using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class WikiPageListing : BaseContainer
    {
        [JsonProperty("data")]
        public List<string> Data { get; set; }
    }
}
