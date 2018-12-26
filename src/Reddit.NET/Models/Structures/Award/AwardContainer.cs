using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class AwardContainer : BaseContainer
    {
        [JsonProperty("data")]
        public Award Data;
    }
}
