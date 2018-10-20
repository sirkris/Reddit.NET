using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class MessageChild
    {
        [JsonProperty("kind")]
        public string Kind;

        [JsonProperty("data")]
        public Message Data;
    }
}
