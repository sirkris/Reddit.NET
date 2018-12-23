using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class PostResult : BaseResult
    {
        [JsonProperty("data")]
        public PostResultData Data;
    }
}
