using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ModmailUnreadCount
    {
        [JsonProperty("highlighted")]
        public int Highlighted;

        [JsonProperty("notifications")]
        public int Notifications;

        [JsonProperty("archived")]
        public int Archived;

        [JsonProperty("new")]
        public int New;

        [JsonProperty("inprogress")]
        public int InProgress;

        [JsonProperty("mod")]
        public int Mod;
    }
}
