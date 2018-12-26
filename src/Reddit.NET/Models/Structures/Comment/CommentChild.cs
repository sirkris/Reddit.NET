using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class CommentChild : BaseContainer
    {
        [JsonProperty("data")]
        public Comment Data;
    }
}
