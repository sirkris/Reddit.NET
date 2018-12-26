using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class UserSubredditContainer
    {
        [JsonProperty("data")]
        public UserSubreddit Data;

        [JsonProperty("name")]
        public string Name;

        public UserSubredditContainer(UserSubreddit data, string name)
        {
            Data = data;
            Name = name;
        }

        public UserSubredditContainer() { }
    }
}
