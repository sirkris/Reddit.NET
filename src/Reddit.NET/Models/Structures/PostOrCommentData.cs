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
    public class PostOrCommentData
    {
        [JsonProperty("modhash")]
        public string Modhash;

        [JsonProperty("dist")]
        public int? Dist;

        [JsonProperty("children")]
        public List<PostOrCommentChild> Children;

        [JsonProperty("after")]
        public string after;

        [JsonProperty("before")]
        public string before;
    }
}
