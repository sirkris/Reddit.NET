using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
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
