using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class UserListContainer : BaseContainer
    {
        [JsonProperty("data")]
        public UserListData Data;
    }
}
