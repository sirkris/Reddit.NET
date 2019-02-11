using Newtonsoft.Json;
using System;

namespace Reddit.Things
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
