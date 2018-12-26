using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class CommentResultContainer
    {
        [JsonProperty("json")]
        public CommentResult JSON;
    }
}
