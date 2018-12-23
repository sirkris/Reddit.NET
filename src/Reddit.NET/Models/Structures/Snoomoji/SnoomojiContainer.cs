using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class SnoomojiContainer
    {
        [JsonProperty("snoomojis")]
        public Dictionary<string, Snoomoji> Snoomojis;
    }
}
