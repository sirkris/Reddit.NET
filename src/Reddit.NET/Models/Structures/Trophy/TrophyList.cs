using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class TrophyList : BaseContainer
    {
        [JsonProperty("data")]
        public TrophiesData Data;
    }
}
