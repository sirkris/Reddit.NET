using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class AwardContainer : BaseContainer
    {
        [JsonProperty("data")]
        public Award Data { get; set; }
    }
}
