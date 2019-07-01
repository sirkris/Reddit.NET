using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class FlairListResult
    {
        [JsonProperty("flair_css_class")]
        public string FlairCssClass { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("flair_text")]
        public string FlairText { get; set; }

        public string ToCSV()
        {
            return User + "," + FlairText + "," + FlairCssClass;
        }
    }
}
