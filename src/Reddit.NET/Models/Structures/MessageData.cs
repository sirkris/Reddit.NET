using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class MessageData : BaseData
    {
        [JsonProperty("children")]
        public List<MessageChild> Children;
    }
}
