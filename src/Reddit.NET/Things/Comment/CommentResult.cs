using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class CommentResult : BaseResult
    {
        [JsonProperty("data")]
        public CommentResultData Data { get; set; }
    }
}
