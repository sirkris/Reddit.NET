using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class CommentChild : BaseContainer
    {
        [JsonProperty("data")]
        public Comment Data { get; set; }
    }
}
