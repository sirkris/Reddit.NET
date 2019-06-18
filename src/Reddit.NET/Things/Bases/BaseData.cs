using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public abstract class BaseData
    {
        [JsonProperty("modhash")]
        public string Modhash { get; set; }

        [JsonProperty("dist")]
        public int? Dist { get; set; }

        [JsonProperty("after")]
        public string after { get; set; }

        [JsonProperty("before")]
        public string before { get; set; }
    }
}
