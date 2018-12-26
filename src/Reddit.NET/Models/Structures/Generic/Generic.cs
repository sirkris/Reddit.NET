using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class Generic : BaseResult
    {
        [JsonProperty("data")]
        public dynamic Data;
    }
}
