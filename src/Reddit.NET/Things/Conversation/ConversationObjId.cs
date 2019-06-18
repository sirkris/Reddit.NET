using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class ConversationObjId
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
