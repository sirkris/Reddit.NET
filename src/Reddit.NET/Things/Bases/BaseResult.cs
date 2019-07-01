using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public abstract class BaseResult
    {
        [JsonProperty("errors")]
        public List<List<string>> Errors { get; set; }

        [JsonProperty("ratelimit")]
        public double Ratelimit { get; set; }
    }
}
