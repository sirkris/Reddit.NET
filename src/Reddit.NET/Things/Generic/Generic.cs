using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class Generic : BaseResult
    {
        [JsonProperty("data")]
        public dynamic Data { get; set; }
    }
}
