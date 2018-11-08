using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class UserPrefsContainer : BaseContainer
    {
        [JsonProperty("data")]
        public UserPrefsData Data;
    }
}
