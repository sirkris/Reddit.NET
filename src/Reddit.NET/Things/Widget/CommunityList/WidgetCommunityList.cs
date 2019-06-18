using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetCommunityList : BaseContainer
    {
        [JsonProperty("data")]
        public List<string> Data { get; set; }  // List of subreddit names.  --Kris

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("styles")]
        public WidgetStyles Styles { get; set; }

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
