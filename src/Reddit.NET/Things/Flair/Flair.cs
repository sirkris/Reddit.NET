using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class Flair
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("text_editable")]
        public bool TextEditable { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("css_class")]
        public string CssClass { get; set; }
    }
}
