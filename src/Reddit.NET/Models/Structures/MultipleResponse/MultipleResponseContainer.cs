using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class MultipleResponseContainer
    {
        [JsonProperty("json")]
        public MultipleResponse JSON;
    }
}
