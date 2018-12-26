using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class MessageContainer : BaseContainer
    {
        [JsonProperty("data")]
        public MessageData Data;
    }
}
