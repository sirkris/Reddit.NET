using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class LiveUpdateChild : BaseContainer
    {
        [JsonProperty("data")]
        public LiveUpdate Data;
    }
}
