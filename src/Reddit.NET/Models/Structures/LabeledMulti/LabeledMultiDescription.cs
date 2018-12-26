using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class LabeledMultiDescription
    {
        [JsonProperty("body_html")]
        public string BodyHTML;

        [JsonProperty("body_md")]
        public string BodyMd;
    }
}
