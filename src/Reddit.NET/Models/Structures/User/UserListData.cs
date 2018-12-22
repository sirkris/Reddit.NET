using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class UserListData
    {
        [JsonProperty("children")]
        public List<UserListChild> Children;
    }
}
