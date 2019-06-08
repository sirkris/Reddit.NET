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
        public bool IsAuto;

        [JsonProperty("objIds")]
        public List<ConversationObjId> ObjIds;

        [JsonProperty("isReplicable")]
        public bool IsReplicable;

        [JsonProperty("lastUserUpdate")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime LastUserUpdate;

        [JsonProperty("isInternal")]
        public bool IsInternal;

        [JsonProperty("lastModUpdate")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime LastModUpdate;

        [JsonProperty("lastUpdated")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime LastUpdated;

        [JsonProperty("authors")]
        public List<ConversationAuthor> Authors;

        [JsonProperty("owner")]
        public ConversationOwner Owner;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("isHighlighted")]
        public bool IsHighlighted;

        [JsonProperty("subject")]
        public string Subject;

        [JsonProperty("participant")]
        public ConversationAuthor Participant;

        [JsonProperty("state")]
        public int State;

        [JsonProperty("lastUnread")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime LastUnread;

        [JsonProperty("numMessages")]
        public int NumMessages;
    }
}
