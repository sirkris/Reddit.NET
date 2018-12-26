using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
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

        public string Fullname => "t2_" + Id;
    }
}
