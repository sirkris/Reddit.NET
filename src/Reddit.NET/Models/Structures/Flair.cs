using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class Flair
    {
        [JsonProperty("text")]
        public string Text;

        [JsonProperty("text_editable")]
        public bool TextEditable;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("css_class")]
        public string CssClass;
    }
}
