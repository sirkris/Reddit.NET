using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class CommentResultData
    {
        [JsonProperty("things")]
        public List<CommentChild> Things { get; set; }
    }
}
