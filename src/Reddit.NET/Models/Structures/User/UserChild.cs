using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class UserChild : BaseContainer
    {
        [JsonProperty("data")]
        public User Data;
    }
}
