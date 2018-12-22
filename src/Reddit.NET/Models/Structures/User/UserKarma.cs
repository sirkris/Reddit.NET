using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
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
