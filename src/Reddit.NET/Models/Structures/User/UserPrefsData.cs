using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class UserPrefsData
    {
        [JsonProperty("children")]
        public List<UserPrefs> Children;
    }
}
