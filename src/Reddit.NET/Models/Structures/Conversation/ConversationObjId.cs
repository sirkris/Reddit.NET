using Newtonsoft.Json;
using System;

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
