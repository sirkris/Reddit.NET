using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class FlairV2
    {
        [JsonProperty("text_editable")]
        public bool TextEditable { get; set; }

        [JsonProperty("textEditable")]
        private bool textEditable
        {
            get { return TextEditable; }
            set { TextEditable = value; }
        }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("mod_only")]
        public bool ModOnly { get; set; }

        [JsonProperty("modOnly")]
        private bool modOnly
        {
            get { return ModOnly; }
            set { ModOnly = value; }
        }

        [JsonProperty("richtext")]
        public List<FlairRichtext> Richtext { get; set; }

        [JsonProperty("background_color")]
        public string BackgroundColor { get; set; }

        [JsonProperty("backgroundColor")]
        private string backgroundColor
        {
            get { return BackgroundColor; }
            set { BackgroundColor = value; }
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text_color")]
        public string TextColor { get; set; }

        [JsonProperty("textColor")]
        private string textColor
        {
            get { return TextColor; }
            set { TextColor = value; }
        }

        [JsonProperty("flair_type")]
        public string FlairType { get; set; }

        [JsonProperty("flairType")]
        private string flairType
        {
            get { return FlairType; }
            set { FlairType = value; }
        }

        public bool ShouldSerializetextEditable()
        {
            return false;
        }

        public bool ShouldSerializemodOnly()
        {
            return false;
        }

        public bool ShouldSerializebackgroundColor()
        {
            return false;
        }

        public bool ShouldSerializetextColor()
        {
            return false;
        }

        public bool ShouldSerializeflairType()
        {
            return false;
        }
    }
}
