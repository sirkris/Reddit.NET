using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class UserSubredditContainer
    {
        [JsonProperty("data")]
        public UserSubreddit Data { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public UserSubredditContainer(UserSubreddit data, string name)
        {
            Data = data;
            Name = name;
        }

        public UserSubredditContainer() { }
    }
}
