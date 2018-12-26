using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class SubredditChild : BaseContainer
    {
        [JsonProperty("data")]
        public Subreddit Data;
    }
}
