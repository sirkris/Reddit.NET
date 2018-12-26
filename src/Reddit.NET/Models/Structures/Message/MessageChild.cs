using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class MessageChild : BaseContainer
    {
        [JsonProperty("data")]
        public Message Data;
    }
}
