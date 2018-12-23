using Newtonsoft.Json;
using Reddit.NET.Models.Converters;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WikiPage
    {
        [JsonProperty("may_revise")]
        public bool MayRevise;

        [JsonProperty("revision_date")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime RevisionDate;

        [JsonProperty("content_html")]
        public string ContentHTML;

        [JsonProperty("revision_by")]
        public UserChild RevisionBy;

        [JsonProperty("content_md")]
        public string ContentMd;
    }
}
