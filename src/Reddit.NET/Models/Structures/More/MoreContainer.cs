using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class MoreContainer : BaseContainer
    {
        [JsonProperty("data")]
        public MoreData Data;
    }
}
