using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class UserListChild
    {
        [JsonProperty("permissions")]
        public List<string> Permissions { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public string Fullname => "t2_" + Id;
    }
}
