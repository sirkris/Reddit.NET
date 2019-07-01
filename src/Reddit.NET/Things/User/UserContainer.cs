using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class UserContainer : BaseContainer
    {
        [JsonProperty("data")]
        public UserData Data { get; set; }
    }
}
