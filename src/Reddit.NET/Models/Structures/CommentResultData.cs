using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class CommentResultData
    {
        [JsonProperty("things")]
        public List<CommentChild> Things;
    }
}
