using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ConversationAuthor
    {
        [JsonProperty("isMod")]
        public bool IsMod;

        [JsonProperty("isAdmin")]
        public bool IsAdmin;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("isOp")]
        public bool IsOp;

        [JsonProperty("isParticipant")]
        public bool IsParticipant;

        [JsonProperty("isHidden")]
        public bool IsHidden;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("isDeleted")]
        public bool IsDeleted;
    }
}
