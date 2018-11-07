using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class FlairListResult
    {
        [JsonProperty("flair_css_class")]
        public string FlairCssClass;

        [JsonProperty("user")]
        public string User;

        [JsonProperty("flair_text")]
        public string FlairText;

        public string ToCSV()
        {
            return User + "," + FlairText + "," + FlairCssClass;
        }
    }
}
