using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public abstract class BaseContainer
    {
        [JsonProperty("kind")]
        public string Kind;
    }
}
