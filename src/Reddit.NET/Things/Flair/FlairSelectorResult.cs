using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class FlairSelectorResult
    {
        [JsonProperty("flair_css_class")]
        public string FlairCssClass { get; set; }

        [JsonProperty("flair_template_id")]
        public string FlairTemplateId { get; set; }

        [JsonProperty("flair_text_editable")]
        public bool FlairTextEditable { get; set; }

        [JsonProperty("flair_position")]
        public string FlairPosition { get; set; }

        [JsonProperty("flair_text")]
        public string FlairText { get; set; }
    }
}
