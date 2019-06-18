using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class ConversationAuthor
    {
        [JsonProperty("isMod")]
        public bool IsMod { get; set; }

        [JsonProperty("isAdmin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isOp")]
        public bool IsOp { get; set; }

        [JsonProperty("isParticipant")]
        public bool IsParticipant { get; set; }

        [JsonProperty("isHidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
