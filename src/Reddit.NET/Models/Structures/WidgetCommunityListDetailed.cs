using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetCommunityListDetailed
    {
        [JsonProperty("data")]
        public List<WidgetCommunityListData> Data;  // List of subreddits.  --Kris

        [JsonProperty("kind")]
        public string Kind;

        [JsonProperty("shortName")]
        public string ShortName;

        [JsonProperty("styles")]
        public WidgetStyles Styles;

        public WidgetCommunityListDetailed(List<WidgetCommunityListData> data, string shortName, WidgetStyles styles)
        {
            Data = data;
            ShortName = shortName;
            Styles = styles;
            Kind = "community-list";
        }

        public WidgetCommunityListDetailed() { }
    }
}
