using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class CommentResult : BaseResult
    {
        [JsonProperty("data")]
        public CommentResultData Data;
    }
}
