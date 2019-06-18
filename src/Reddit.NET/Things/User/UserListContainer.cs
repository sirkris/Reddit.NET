using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class UserListContainer : BaseContainer
    {
        [JsonProperty("data")]
        public UserListData Data { get; set; }
    }
}
