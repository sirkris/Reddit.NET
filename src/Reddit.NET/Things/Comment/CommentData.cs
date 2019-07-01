using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class CommentData : BaseData
    {
        [JsonProperty("children")]
        public List<CommentChild> Children { get; set; }
    }
}
