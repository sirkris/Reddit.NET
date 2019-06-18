using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class LabeledMultiDescription
    {
        [JsonProperty("body_html")]
        public string BodyHTML { get; set; }

        [JsonProperty("body_md")]
        public string BodyMd { get; set; }
    }
}
