using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class SubredditName
    {
        [JsonProperty("name")]
        public string Name;

        public SubredditName(string name)
        {
            Name = name;
        }

        public SubredditName() { }
    }
}
