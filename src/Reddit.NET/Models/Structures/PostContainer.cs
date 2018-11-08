using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class PostContainer : BaseContainer
    {
        [JsonProperty("data")]
        public PostData Data;
    }
}
