using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class UserPrefsData
    {
        [JsonProperty("children")]
        public List<UserPrefs> Children { get; set; }
    }
}
