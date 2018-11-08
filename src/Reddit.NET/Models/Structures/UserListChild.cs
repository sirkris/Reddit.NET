using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class UserListChild
    {
        [JsonProperty("permissions")]
        public List<string> Permissions;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;
    }
}
