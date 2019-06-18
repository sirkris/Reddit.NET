using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class LiveUpdateChild : BaseContainer
    {
        [JsonProperty("data")]
        public LiveUpdate Data { get; set; }
    }
}
