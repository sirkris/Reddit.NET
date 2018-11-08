using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class PostChild : BaseContainer
    {
        [JsonProperty("data")]
        public Post Data;
    }
}
