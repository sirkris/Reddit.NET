using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class ModmailSubredditContainer
    {
        [JsonProperty("subreddits")]
        public Dictionary<string, ModmailSubreddit> Subreddits;
    }
}
