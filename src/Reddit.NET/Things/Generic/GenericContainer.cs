using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class GenericContainer
    {
        [JsonProperty("json")]
        public Generic JSON { get; set; }
    }
}
