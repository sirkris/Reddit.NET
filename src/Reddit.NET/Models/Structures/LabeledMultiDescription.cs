using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
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
