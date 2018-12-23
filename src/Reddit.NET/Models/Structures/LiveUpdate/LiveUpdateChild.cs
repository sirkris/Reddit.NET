using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class LiveUpdateChild : BaseContainer
    {
        [JsonProperty("data")]
        public LiveUpdate Data;
    }
}
