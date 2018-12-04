using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class NamedObj
    {
        [JsonProperty("name")]
        public string Name;
    }
}
