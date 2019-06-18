using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class CommentResultContainer
    {
        [JsonProperty("json")]
        public CommentResult JSON { get; set; }
    }
}
