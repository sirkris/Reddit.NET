using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class CommentContainer : BaseContainer
    {
        [JsonProperty("data")]
        public CommentData Data;
    }
}
