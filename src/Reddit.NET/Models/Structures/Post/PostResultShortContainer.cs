using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class PostResultShortContainer
    {
        [JsonProperty("json")]
        public PostResultShort JSON;
    }
}
