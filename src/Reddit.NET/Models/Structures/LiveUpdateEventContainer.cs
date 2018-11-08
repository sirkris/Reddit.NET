using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class LiveUpdateEventContainer : BaseContainer
    {
        [JsonProperty("data")]
        public LiveUpdateEvent Data;
    }
}
