using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class GenericContainer
    {
        [JsonProperty("json")]
        public Generic JSON;
    }
}
