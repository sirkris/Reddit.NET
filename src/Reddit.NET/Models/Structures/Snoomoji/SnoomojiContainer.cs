using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class SnoomojiContainer
    {
        [JsonProperty("snoomojis")]
        public Dictionary<string, Snoomoji> Snoomojis;
    }
}
