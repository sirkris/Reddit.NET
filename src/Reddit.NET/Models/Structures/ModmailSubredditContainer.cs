using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ModmailSubredditContainer
    {
        [JsonProperty("subreddits")]
        public Dictionary<string, ModmailSubreddit> Subreddits;
    }
}
