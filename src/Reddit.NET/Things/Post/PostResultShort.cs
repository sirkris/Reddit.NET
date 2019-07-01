using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class PostResultShort : BaseResult
    {
        [JsonProperty("data")]
        public PostResultShortData Data { get; set; }
    }
}
