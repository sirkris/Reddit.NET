using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class UserKarma
    {
        [JsonProperty("comment_karma")]
        public int CommentKarma;

        [JsonProperty("link_karma")]
        public int LinkKarma;

        [JsonProperty("sr")]
        public string Sr;
    }
}
