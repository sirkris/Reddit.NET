using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WikiPage
    {
        [JsonProperty("may_revise")]
        public bool MayRevise { get; set; }

        [JsonProperty("revision_date")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime RevisionDate { get; set; }

        [JsonProperty("content_html")]
        public string ContentHTML { get; set; }

        [JsonProperty("revision_by")]
        public UserChild RevisionBy { get; set; }

        [JsonProperty("content_md")]
        public string ContentMd { get; set; }
    }
}
