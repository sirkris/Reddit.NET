using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class CommentResult : BaseResult
    {
        [JsonProperty("data")]
        public CommentResultData Data;
    }
}
