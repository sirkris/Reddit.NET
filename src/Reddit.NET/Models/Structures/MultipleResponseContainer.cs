using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class MultipleResponseContainer
    {
        [JsonProperty("json")]
        public MultipleResponse JSON;
    }
}
