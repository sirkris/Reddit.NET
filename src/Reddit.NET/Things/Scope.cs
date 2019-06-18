using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class Scope
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
