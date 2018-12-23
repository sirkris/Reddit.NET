using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class CommentResultContainer
    {
        [JsonProperty("json")]
        public CommentResult JSON;
    }
}
