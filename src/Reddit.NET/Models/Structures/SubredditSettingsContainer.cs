using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class SubredditSettingsContainer : BaseContainer
    {
        [JsonProperty("data")]
        public SubredditSettings Data;
    }
}
