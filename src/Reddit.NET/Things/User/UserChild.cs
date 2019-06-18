using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class UserChild : BaseContainer
    {
        [JsonProperty("data")]
        public User Data { get; set; }
    }
}
