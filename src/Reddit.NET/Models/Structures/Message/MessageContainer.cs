using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class MessageContainer : BaseContainer
    {
        [JsonProperty("data")]
        public MessageData Data;
    }
}
