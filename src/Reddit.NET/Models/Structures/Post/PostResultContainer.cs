using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class PostResultContainer
    {
        [JsonProperty("json")]
        public PostResult JSON;
    }
}
