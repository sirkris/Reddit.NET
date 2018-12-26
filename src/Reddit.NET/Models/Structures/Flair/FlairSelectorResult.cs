using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class FlairSelectorResult
    {
        [JsonProperty("flair_css_class")]
        public string FlairCssClass;

        [JsonProperty("flair_template_id")]
        public string FlairTemplateId;

        [JsonProperty("flair_text_editable")]
        public bool FlairTextEditable;

        [JsonProperty("flair_position")]
        public string FlairPosition;

        [JsonProperty("flair_text")]
        public string FlairText;
    }
}
