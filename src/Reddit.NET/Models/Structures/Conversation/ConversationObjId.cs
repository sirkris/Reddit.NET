using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
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
