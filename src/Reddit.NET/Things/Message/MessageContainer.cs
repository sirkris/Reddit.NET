using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class MessageContainer : BaseContainer
    {
        [JsonProperty("data")]
        public MessageData Data { get; set; }
    }
}
