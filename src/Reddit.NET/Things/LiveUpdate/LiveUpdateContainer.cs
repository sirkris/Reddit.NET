using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class LiveUpdateContainer : BaseContainer
    {
        [JsonProperty("data")]
        public LiveUpdateData Data { get; set; }
    }
}
