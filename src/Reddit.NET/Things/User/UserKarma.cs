using Newtonsoft.Json;
using System;

namespace Reddit.Things
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
