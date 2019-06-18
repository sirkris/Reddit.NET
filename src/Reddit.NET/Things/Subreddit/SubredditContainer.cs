using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class SubredditContainer : BaseContainer
    {
        [JsonProperty("data")]
        public SubredditData Data { get; set; }
    }
}
