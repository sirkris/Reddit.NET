using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class PostResult : BaseResult
    {
        [JsonProperty("data")]
        public PostResultData Data { get; set; }
    }
}
