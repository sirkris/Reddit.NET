using Newtonsoft.Json;
using Reddit.NET.Models.Converters;
using System;

namespace Reddit.NET.Models.Structures
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
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime Date;

        [JsonProperty("bodyMarkdown")]
        public string BodyMarkdown;

        [JsonProperty("id")]
        public string Id;
    }
}
