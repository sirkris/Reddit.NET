using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class LiveUpdateContainer : BaseContainer
    {
        [JsonProperty("data")]
        public LiveUpdateData Data;
    }
}
