using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class TrophiesData
    {
        [JsonProperty("trophies")]
        public List<AwardContainer> Trophies;
    }
}
