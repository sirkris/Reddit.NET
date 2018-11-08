using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class PostResultShort : BaseResult
    {
        [JsonProperty("data")]
        public PostResultShortData Data;
    }
}
