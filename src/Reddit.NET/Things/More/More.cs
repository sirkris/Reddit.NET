using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class More
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("depth")]
        public int Depth { get; set; }

        [JsonProperty("children")]
        public List<string> Children { get; set; }
    }
}
