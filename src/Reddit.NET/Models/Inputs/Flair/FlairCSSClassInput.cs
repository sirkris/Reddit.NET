using Newtonsoft.Json;
using System;

namespace Reddit.Models.Inputs.Flair
{
    [Serializable]
    public class FlairCSSClassInput : FlairTextInput
    {
        /// <summary>
        /// a valid subreddit image name
        /// </summary>
        [JsonProperty("css_class")]
        public string css_class { get; set; }
    }
}
