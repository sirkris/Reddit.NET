using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class SubredditChild : BaseContainer
    {
        [JsonProperty("data")]
        public Subreddit Data;
    }
}
