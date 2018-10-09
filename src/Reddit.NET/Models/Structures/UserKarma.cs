using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.NET.Controllers;
using Reddit.NET.Models.Converters;
using System;
using System.Collections.Generic;
using System.Text;

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
