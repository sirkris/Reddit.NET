using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class ModmailUnreadCount
    {
        [JsonProperty("highlighted")]
        public int Highlighted { get; set; }

        [JsonProperty("notifications")]
        public int Notifications { get; set; }

        [JsonProperty("archived")]
        public int Archived { get; set; }

        [JsonProperty("new")]
        public int New { get; set; }

        [JsonProperty("inprogress")]
        public int InProgress { get; set; }

        [JsonProperty("mod")]
        public int Mod { get; set; }
    }
}
