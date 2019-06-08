using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class ConversationMessage
    {
        [JsonProperty("body")]
        public string Body;

        [JsonProperty("author")]
        public ConversationAuthor Author;

        [JsonProperty("isInternal")]
        public bool IsInternal;

        [JsonProperty("date")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime Date;

        [JsonProperty("bodyMarkdown")]
        public string BodyMarkdown;

        [JsonProperty("id")]
        public string Id;
    }
}
