using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class MultipleResponse : BaseResult
    {
        [JsonProperty("data")]
        public MultipleResponseData Data;
    }
}
