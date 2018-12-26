using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class MoreData
    {
        [JsonProperty("count")]
        public int Count;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("parent_id")]
        public string ParentId;

        [JsonProperty("depth")]
        public int Depth;

        [JsonProperty("children")]
        public object Children;  // TODO - Determine type.  --Kris
    }
}
