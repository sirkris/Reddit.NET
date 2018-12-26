using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class TrophiesData
    {
        [JsonProperty("trophies")]
        public List<AwardContainer> Trophies;
    }
}
