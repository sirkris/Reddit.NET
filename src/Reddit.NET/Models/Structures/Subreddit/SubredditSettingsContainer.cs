using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class SubredditSettingsContainer : BaseContainer
    {
        [JsonProperty("data")]
        public SubredditSettings Data;
    }
}
