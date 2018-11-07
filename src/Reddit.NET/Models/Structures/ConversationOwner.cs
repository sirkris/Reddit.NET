using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ConversationOwner
    {
        [JsonProperty("displayName")]
        public string DisplayName;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public string Id;
    }
}
