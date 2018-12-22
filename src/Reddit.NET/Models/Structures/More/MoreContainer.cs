using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class MoreContainer : BaseContainer
    {
        [JsonProperty("data")]
        public MoreData Data;
    }
}
