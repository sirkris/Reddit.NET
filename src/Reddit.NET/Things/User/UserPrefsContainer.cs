using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class UserPrefsContainer : BaseContainer
    {
        [JsonProperty("data")]
        public UserPrefsData Data { get; set; }
    }
}
