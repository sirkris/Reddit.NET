using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class LiveUpdate
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("mobile_embeds")]
        public object MobileEmbeds { get; set; }  // TODO - Determine type.  --Kris

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("embeds")]
        public object Embeds { get; set; }  // TODO - Determine type.  --Kris

        [JsonProperty("created")]
        [JsonConverter(typeof(LocalTimestampConverter))]
        [Obsolete("Using this date can lead to unexpected results.  It is recommended that you use " + nameof(CreatedUTC) + " instead.")]
        public DateTime Created { get; set; }

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC { get; set; }

        [JsonProperty("body_html")]
        public string BodyHTML { get; set; }

        [JsonProperty("stricken")]
        public bool Stricken { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        public string Fullname => "LiveUpdate_" + Id;
    }
}
