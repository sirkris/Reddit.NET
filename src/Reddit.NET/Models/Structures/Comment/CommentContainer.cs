using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class CommentContainer : BaseContainer
    {
        [JsonProperty("data")]
        public CommentData Data;
    }
}
