using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class CommentData : BaseData
    {
        [JsonProperty("children")]
        public List<CommentChild> Children;
    }
}
