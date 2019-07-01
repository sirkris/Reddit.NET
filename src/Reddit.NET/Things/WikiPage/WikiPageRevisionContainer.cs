using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WikiPageRevisionContainer : BaseContainer
    {
        [JsonProperty("data")]
        public WikiPageRevisionData Data { get; set; }
    }
}
