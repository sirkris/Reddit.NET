using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public abstract class BaseContainer
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }
    }
}
