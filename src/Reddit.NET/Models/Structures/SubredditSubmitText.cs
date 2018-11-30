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

        public SubredditSubmitText(string submitText, string submitTextHtml)
        {
            SubmitText = submitText;
            SubmitTextHTML = submitTextHtml;
        }

        public SubredditSubmitText(string submitText)
        {
            SubmitText = submitText;
            SubmitTextHTML = submitText;
        }

        public SubredditSubmitText() { }

        public override string ToString()
        {
            return SubmitText;
        }
    }
}
