using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class MoreChild : BaseContainer
    {
        [JsonProperty("data")]
        public More Data { get; set; }
    }
}
