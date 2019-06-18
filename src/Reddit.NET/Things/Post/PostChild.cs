using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class PostChild : BaseContainer
    {
        [JsonProperty("data")]
        public Post Data { get; set; }
    }
}
