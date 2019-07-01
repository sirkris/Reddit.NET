using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class MessageData : BaseData
    {
        [JsonProperty("children")]
        public List<MessageChild> Children { get; set; }
    }
}
