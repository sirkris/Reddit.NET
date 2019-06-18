using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class PostContainer : BaseContainer
    {
        [JsonProperty("data")]
        public PostData Data { get; set; }
    }
}
