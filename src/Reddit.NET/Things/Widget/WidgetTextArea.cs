using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetTextArea : BaseContainer
    {
        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("styles")]
        public WidgetStyles Styles { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("textHtml")]
        public string TextHTML
        {
            get;
            private set;
        }

        [JsonProperty("id")]
        public string Id
        {
            get;
            private set;
        }

        public WidgetTextArea(string shortName, WidgetStyles styles, string text)
        {
            Import(shortName, styles, text);
        }

        public WidgetTextArea(string shortName, string text)
        {
            Import(shortName, new WidgetStyles(), text);
        }

        public WidgetTextArea() { }

        private void Import(string shortName, WidgetStyles styles, string text)
        {
            ShortName = shortName;
            Styles = styles;
            Text = text;
            Kind = "textarea";
        }
    }
}
