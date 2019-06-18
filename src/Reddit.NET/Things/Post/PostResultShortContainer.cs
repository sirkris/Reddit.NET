using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class PostResultShortContainer
    {
        [JsonProperty("json")]
        public PostResultShort JSON { get; set; }
    }
}
