using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class TrophiesData
    {
        [JsonProperty("trophies")]
        public List<AwardContainer> Trophies { get; set; }
    }
}
