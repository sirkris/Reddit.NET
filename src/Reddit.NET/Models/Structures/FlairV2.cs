using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class FlairV2
    {
        [JsonProperty("text_editable")]
        public bool TextEditable;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("text")]
        public string Text;

        [JsonProperty("mod_only")]
        public bool ModOnly;

        [JsonProperty("richtext")]
        public List<FlairRichtext> Richtext;

        [JsonProperty("background_color")]
        public string BackgroundColor;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("text_color")]
        public string TextColor;
    }
}
