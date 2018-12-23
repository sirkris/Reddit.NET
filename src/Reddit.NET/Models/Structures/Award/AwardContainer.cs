using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class AwardContainer : BaseContainer
    {
        [JsonProperty("data")]
        public Award Data;
    }
}
