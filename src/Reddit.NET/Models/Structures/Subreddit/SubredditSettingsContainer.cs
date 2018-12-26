using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class SubredditSettingsContainer : BaseContainer
    {
        [JsonProperty("data")]
        public SubredditSettings Data;
    }
}
