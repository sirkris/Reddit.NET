using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class LiveUpdateContainer : BaseContainer
    {
        [JsonProperty("data")]
        public LiveUpdateData Data;
    }
}
