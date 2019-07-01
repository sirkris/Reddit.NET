using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class MoreContainer : BaseContainer
    {
        [JsonProperty("data")]
        public MoreData Data { get; set; }
    }
}
