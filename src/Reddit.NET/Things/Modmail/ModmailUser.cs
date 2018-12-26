using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class ModmailUser
    {
        [JsonProperty("recentComments")]
        public object RecentComments;  // TODO - Determine type.  --Kris

        [JsonProperty("muteStatus")]
        public MuteStatus MuteStatus;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("created")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime Created;

        [JsonProperty("banStatus")]
        public BanStatus BanStatus;

        [JsonProperty("isSuspended")]
        public bool IsSuspended;

        [JsonProperty("isShadowBanned")]
        public bool IsShadowBanned;

        [JsonProperty("recentPosts")]
        public object RecentPosts;  // TODO - Determine type.  --Kris

        [JsonProperty("recentConvos")]
        public Dictionary<string, Convo> RecentConvos;

        [JsonProperty("id")]
        public string Id;
    }
}
