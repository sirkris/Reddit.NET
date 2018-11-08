using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class SubredditSubmitText
    {
        [JsonProperty("submit_text")]
        public string SubmitText;

        [JsonProperty("submit_text_html")]
        public string SubmitTextHTML;
    }
}
