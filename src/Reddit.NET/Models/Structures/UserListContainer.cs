using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class UserListContainer : BaseContainer
    {
        [JsonProperty("data")]
        public UserListData Data;
    }
}
