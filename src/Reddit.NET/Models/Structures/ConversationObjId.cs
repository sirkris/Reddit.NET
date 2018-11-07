using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ConversationObjId
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("key")]
        public string Key;
    }
}
