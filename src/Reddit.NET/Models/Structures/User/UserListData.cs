using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class UserListData
    {
        [JsonProperty("children")]
        public List<UserListChild> Children;
    }
}
