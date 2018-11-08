using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class FlairV2
    {
        [JsonProperty("text_editable")]
        public bool TextEditable;

        [JsonProperty("textEditable")]
        private bool textEditable
        {
            get { return TextEditable; }
            set { TextEditable = value; }
        }

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("text")]
        public string Text;

        [JsonProperty("mod_only")]
        public bool ModOnly;

        [JsonProperty("modOnly")]
        private bool modOnly
        {
            get { return ModOnly; }
            set { ModOnly = value; }
        }

        [JsonProperty("richtext")]
        public List<FlairRichtext> Richtext;

        [JsonProperty("background_color")]
        public string BackgroundColor;

        [JsonProperty("backgroundColor")]
        private string backgroundColor
        {
            get { return BackgroundColor; }
            set { BackgroundColor = value; }
        }

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("text_color")]
        public string TextColor;

        [JsonProperty("textColor")]
        private string textColor
        {
            get { return TextColor; }
            set { TextColor = value; }
        }

        [JsonProperty("flair_type")]
        public string FlairType;

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
