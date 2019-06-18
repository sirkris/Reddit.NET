using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class CommentContainer : BaseContainer
    {
        [JsonProperty("data")]
        public CommentData Data { get; set; }
    }
}
