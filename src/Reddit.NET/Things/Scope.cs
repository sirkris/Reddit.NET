using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class Scope
    {
        [JsonProperty("description")]
        public string Description;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;
    }
}
