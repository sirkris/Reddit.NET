using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class SubredditSubmitText
    {
        [JsonProperty("submit_text")]
        public string SubmitText { get; set; }

        [JsonProperty("submit_text_html")]
        public string SubmitTextHTML { get; set; }

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
