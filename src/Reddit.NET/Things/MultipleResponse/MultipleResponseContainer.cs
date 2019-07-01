using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class MultipleResponseContainer
    {
        [JsonProperty("json")]
        public MultipleResponse JSON { get; set; }
    }
}
