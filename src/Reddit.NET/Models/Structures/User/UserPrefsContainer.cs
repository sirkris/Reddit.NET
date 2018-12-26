using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class UserPrefsContainer : BaseContainer
    {
        [JsonProperty("data")]
        public UserPrefsData Data;
    }
}
