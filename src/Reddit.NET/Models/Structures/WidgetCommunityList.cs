using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetCommunityList : BaseContainer
    {
        [JsonProperty("data")]
        public List<string> Data;  // List of subreddit names.  --Kris

        [JsonProperty("shortName")]
        public string ShortName;

        [JsonProperty("styles")]
        public WidgetStyles Styles;

        public WidgetCommunityList(List<string> data, string shortName, WidgetStyles styles)
        {
            Data = data;
            ShortName = shortName;
            Styles = styles;
            Kind = "community-list";
        }

        public WidgetCommunityList() { }
    }
}
