using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class UserKarma
    {
        [JsonProperty("comment_karma")]
        public int CommentKarma { get; set; }

        [JsonProperty("link_karma")]
        public int LinkKarma { get; set; }

        [JsonProperty("sr")]
        public string Sr { get; set; }
    }
}
