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
        public object RecentComments { get; set; }  // TODO - Determine type.  --Kris

        [JsonProperty("muteStatus")]
        public MuteStatus MuteStatus { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime Created { get; set; }

        [JsonProperty("banStatus")]
        public BanStatus BanStatus { get; set; }

        [JsonProperty("isSuspended")]
        public bool IsSuspended { get; set; }

        [JsonProperty("isShadowBanned")]
        public bool IsShadowBanned { get; set; }

        [JsonProperty("recentPosts")]
        public object RecentPosts { get; set; }  // TODO - Determine type.  --Kris

        [JsonProperty("recentConvos")]
        public Dictionary<string, Convo> RecentConvos { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
