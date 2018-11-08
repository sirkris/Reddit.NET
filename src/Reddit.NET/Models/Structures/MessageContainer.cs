using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class MessageContainer : BaseContainer
    {
        [JsonProperty("data")]
        public MessageData Data;
    }
}
