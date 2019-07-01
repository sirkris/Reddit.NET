using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class ModmailSubredditContainer
    {
        [JsonProperty("subreddits")]
        public Dictionary<string, ModmailSubreddit> Subreddits { get; set; }
    }
}
