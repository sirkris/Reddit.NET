using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class PostResultContainer
    {
        [JsonProperty("json")]
        public PostResult JSON { get; set; }
    }
}
