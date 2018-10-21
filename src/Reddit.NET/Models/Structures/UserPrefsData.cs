using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class UserPrefsData
    {
        [JsonProperty("children")]
        public List<UserPrefs> Children;
    }
}
