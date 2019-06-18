using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class SubredditChild : BaseContainer
    {
        [JsonProperty("data")]
        public Subreddit Data { get; set; }
    }
}
