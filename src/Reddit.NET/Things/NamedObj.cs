using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class NamedObj
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
