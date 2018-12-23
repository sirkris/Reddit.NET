using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class GenericContainer
    {
        [JsonProperty("json")]
        public Generic JSON;
    }
}
