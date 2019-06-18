using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class MultipleResponse : BaseResult
    {
        [JsonProperty("data")]
        public MultipleResponseData Data { get; set; }
    }
}