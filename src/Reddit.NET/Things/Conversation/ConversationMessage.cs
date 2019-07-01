using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class ConversationMessage
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("author")]
        public ConversationAuthor Author { get; set; }

        [JsonProperty("isInternal")]
        public bool IsInternal { get; set; }

        [JsonProperty("date")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime Date { get; set; }

        [JsonProperty("bodyMarkdown")]
        public string BodyMarkdown { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
