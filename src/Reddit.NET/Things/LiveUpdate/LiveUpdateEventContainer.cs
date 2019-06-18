using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class LiveUpdateEventContainer : BaseContainer
    {
        [JsonProperty("data")]
        public LiveUpdateEvent Data { get; set; }
    }
}
