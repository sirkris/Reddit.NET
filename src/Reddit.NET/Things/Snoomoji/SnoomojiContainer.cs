﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reddit.Things
{
    [Serializable]
    public class SnoomojiContainer
    {
        [JsonProperty("snoomojis")]
        public Dictionary<string, Snoomoji> Snoomojis { get; set; }

        public Dictionary<string, Snoomoji> SubredditEmojis =>
                ExtraFields.Values.FirstOrDefault()?.ToObject<Dictionary<string, Snoomoji>>();

        [JsonExtensionData]
        public Dictionary<string, JToken> ExtraFields { get; set; }
    }
}
