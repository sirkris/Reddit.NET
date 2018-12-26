using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class CommentResult : BaseResult
    {
        [JsonProperty("data")]
        public CommentResultData Data;
    }
}
