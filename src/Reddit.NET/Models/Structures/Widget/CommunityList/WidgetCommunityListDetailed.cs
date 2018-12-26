using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WidgetCommunityListDetailed : BaseContainer
    {
        [JsonProperty("data")]
        public List<WidgetCommunityListData> Data;  // List of subreddits.  --Kris

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
