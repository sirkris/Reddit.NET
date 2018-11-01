using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
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
