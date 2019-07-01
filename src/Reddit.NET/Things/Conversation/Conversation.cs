using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class Conversation
    {
        [JsonProperty("isAuto")]
        public bool IsAuto { get; set; }

        [JsonProperty("objIds")]
        public List<ConversationObjId> ObjIds { get; set; }

        [JsonProperty("isReplicable")]
        public bool IsReplicable { get; set; }

        [JsonProperty("lastUserUpdate")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime LastUserUpdate { get; set; }

        [JsonProperty("isInternal")]
        public bool IsInternal { get; set; }

        [JsonProperty("lastModUpdate")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime LastModUpdate { get; set; }

        [JsonProperty("lastUpdated")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("authors")]
        public List<ConversationAuthor> Authors { get; set; }

        [JsonProperty("owner")]
        public ConversationOwner Owner { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("isHighlighted")]
        public bool IsHighlighted { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("participant")]
        public ConversationAuthor Participant { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("lastUnread")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime LastUnread { get; set; }

        [JsonProperty("numMessages")]
        public int NumMessages { get; set; }
    }
}
