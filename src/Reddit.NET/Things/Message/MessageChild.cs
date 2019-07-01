using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class MessageChild : BaseContainer
    {
        [JsonProperty("data")]
        public Message Data { get; set; }
    }
}
