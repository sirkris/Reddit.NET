using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class Generic : BaseResult
    {
        [JsonProperty("data")]
        public dynamic Data;
    }
}
