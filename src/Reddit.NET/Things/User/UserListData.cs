using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class UserListData
    {
        [JsonProperty("children")]
        public List<UserListChild> Children { get; set; }
    }
}
