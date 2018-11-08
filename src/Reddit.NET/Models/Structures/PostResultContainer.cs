using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class PostResultContainer
    {
        [JsonProperty("json")]
        public PostResult JSON;
    }
}
