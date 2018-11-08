using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class PostResultShortContainer
    {
        [JsonProperty("json")]
        public PostResultShort JSON;
    }
}
