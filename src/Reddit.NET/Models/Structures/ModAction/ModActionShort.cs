using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class ModActionShort
    {
        [JsonProperty("date")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime Date;

        [JsonProperty("actionTypeId")]
        public int ActionTypeId;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("author")]
        public ConversationAuthor Author;
    }
}
