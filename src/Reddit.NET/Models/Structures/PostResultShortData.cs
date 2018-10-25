using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class PostResultShortData
    {
        [JsonProperty("url")]
        public string URL;

        [JsonProperty("drafts_count")]
        public int DraftsCount;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;
    }
}
