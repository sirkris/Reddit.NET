using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WikiPageContainer : BaseContainer
    {
        [JsonProperty("data")]
        public WikiPage Data { get; set; }
    }
}
